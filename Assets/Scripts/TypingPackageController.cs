using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TypingPackageController : MonoBehaviour
{

  private string definedWord = default;
  private Transform target = default;
  private int damage = 1;
  private int correctCount = 0;
  [SerializeField]
  private float wordSpacing = 0;
  [SerializeField]
  private int mode = 0; //mode 0 is not changing space / mode 1 is changing space

  private bool setupReady = false;
  private TypingDataReceiver receiver = default;
  public delegate void OnComplete();
  public event OnComplete onComplete;
  private ScoreData scoreData;
  private ComboData comboData;

    [Header("Lerp Movement")]
    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float desiredDuration = 5;
    private float elapsedTime;
    [SerializeField] AnimationCurve curve;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
  {
        endPosition = target.position;

    if (mode == 1)
    {
      Vector3 lastTransform = default;
      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);
        if (i > 0) child.transform.localPosition = new Vector3(lastTransform.x + wordSpacing, 0f, 0f);
        lastTransform = child.transform.localPosition;
      }
    }
  }

  private void Launch()
  {

  }

  private IEnumerator MoveToPlayer()
  {
    yield return null;
  }

  private void FixedUpdate()
  {
    if (setupReady) MovementBehavior();
  }

  private void Setup(WordObject wordObject, TypingDataReceiver receiver, Transform target)
  {
    Vector3 lastTransform = default;
    desiredDuration = wordObject.durationToTarget;
    definedWord = wordObject.word.ToUpper();
    damage = wordObject.damage;
    this.target = target;
    for(int i = 0; i < definedWord.Length; i++)
    {
      GameObject typingChar = Instantiate(Resources.Load<GameObject>(Const.TYPING_CHAR_PATH));
      typingChar.transform.SetParent(this.transform, false);
      if (i > 0) typingChar.transform.localPosition = new Vector3(lastTransform.x + wordSpacing, 0f, 0f);
      lastTransform = typingChar.transform.localPosition;
      typingChar.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
      typingChar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Const.KEYUP_PATH + definedWord[i]);
    }
    this.receiver = receiver;
    this.receiver.typingDataReceiver += InputListenner;
    setupReady = true;
  }

  public void Setup(WordObject wordObject, TypingDataReceiver receiver, ScoreData scoreData, ComboData comboData, Transform target, OnComplete onComplete)
  {
    Setup(wordObject, receiver, target);
    this.onComplete = onComplete;
    this.scoreData = scoreData;
    this.comboData = comboData;
  }

  public int GetDamage()
  {
    return damage;
  }

  public void ResetState()
  {
    correctCount = 0;
    for (int i = 0; i < transform.childCount; i++)
    {
      transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Const.KEYUP_PATH + definedWord[i]);
    }
  }

  private void InputListenner(char input)
  {
    if (input.Equals(definedWord[correctCount]))
    {
      transform.GetChild(correctCount).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Const.KEYDown_PATH + definedWord[correctCount]);
      correctCount++;
    }

    if (correctCount == definedWord.Length) //typing complete
    {
      this.receiver.typingDataReceiver -= (InputListenner);
      scoreData.UpdateScore(definedWord.Length);
      comboData.UpdateScore(definedWord.Length * 2);
      if (FindObjectOfType<AudioManager>() != null) FindObjectOfType<AudioManager>().Play(definedWord.ToLower());
      onComplete?.Invoke();
    };
  }

  private void MovementBehavior()
  {
    if (target != null)
    {
            //if (!(Vector3.Distance(transform.position, target.position) < 1f))
            //{
            //  _PlayerDirection = Vector3.Normalize(new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y));
            //  transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * speed), transform.position.y - (_PlayerDirection.y * speed), 0.0f);
            //}

            elapsedTime += Time.deltaTime;
            float percentageCompelete = elapsedTime / desiredDuration;

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageCompelete));
    }
  }

  public void CleanDestoy() //can't type in time
  {
    this.receiver.typingDataReceiver -= (InputListenner);
    comboData.UpdateScore(- definedWord.Length * 20);
    Destroy(gameObject);
  }
}