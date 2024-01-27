using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderKeywordFilter;
using UnityEditor.U2D;
using UnityEngine;

public class HealPackageController : MonoBehaviour
{

  private string definedWord = default;
  private Transform target = default;
  private float speed = 0.03f;
  private int damage = 1;
  private Vector3 _PlayerDirection = default;
  private int correctCount = 0;
  [SerializeField]
  private float wordSpacing = 0;
  [SerializeField]
  private int mode = 1; //mode 0 is not changing space / mode 1 is changing space

  private bool setupReady = false;
  private TypingDataReceiver receiver = default;
  public delegate void OnComplete();
  public event OnComplete onComplete;
  private void Update()
  {
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
    speed = wordObject.speed;
    definedWord = wordObject.word.ToUpper();
    damage = wordObject.damage;
    this.target = target;
    for (int i = 0; i < definedWord.Length; i++)
    {
      GameObject typingChar = Instantiate(Resources.Load<GameObject>(Const.TYPING_CHAR_PATH));
      typingChar.transform.SetParent(this.transform, false);
      if (i > 0) typingChar.transform.localPosition = new Vector3(lastTransform.x + wordSpacing, 0f, 0f);
      lastTransform = typingChar.transform.localPosition;
      typingChar.transform.localScale = new Vector3(0.05f, 0.035f, 0.0f);
      typingChar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(Const.KEYUP_PATH + definedWord[i]);
    }
    this.receiver = receiver;
    this.receiver.typingDataReceiver += InputListenner;
    setupReady = true;
  }

  public void Setup(WordObject wordObject, TypingDataReceiver receiver, Transform target, OnComplete onComplete)
  {
    Setup(wordObject, receiver, target);
    this.onComplete = onComplete;
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
      onComplete?.Invoke();
    };
  }

  private void MovementBehavior()
  {
    if (target != null)
    {
      if (!(Vector3.Distance(transform.position, target.position) < 1f))
      {
        _PlayerDirection = Vector3.Normalize(new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y));
        transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * speed), transform.position.y - (_PlayerDirection.y * speed), 0.0f);
      }
    }
  }
}
