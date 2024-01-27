using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEditor.U2D;
using UnityEngine;

public class TypingPackageController : MonoBehaviour
{
  private readonly string KEYUP_PATH = "KeyboardSprite/BlackVariant/";
  private readonly string KEYDown_PATH = "KeyboardSprite/WhiteVariant/";
  private readonly string TYPING_CHAR_PATH = "Prefab/TypingCharacter";
  private string definedWord = default;
  private Transform target = default;
  private float speed = 0.03f;
  private Vector3 _PlayerDirection = default;
  private int correctCount = 0;
  [SerializeField]
  private float wordSpacing = 0;
  [SerializeField]
  private int mode = 0; //mode 0 is not changing space / mode 1 is changing space

  private bool setupReady = false;
  private TypingDataReceiver receiver = default;
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

  public void Setup(WordObject wordObject, TypingDataReceiver receiver, Transform target)
  {
    Vector3 lastTransform = default;
    speed = wordObject.speed;
    definedWord = wordObject.word.ToUpper();
    this.target = target;
    for(int i = 0; i < definedWord.Length; i++)
    {
      GameObject typingChar = Instantiate(Resources.Load<GameObject>(TYPING_CHAR_PATH));
      typingChar.transform.SetParent(this.transform, false);
      if (i > 0) typingChar.transform.localPosition = new Vector3(lastTransform.x + wordSpacing, 0f, 0f);
      lastTransform = typingChar.transform.localPosition;
      typingChar.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
      typingChar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KEYUP_PATH + definedWord[i]);
    }
    this.receiver = receiver;
    this.receiver.typingDataReceiver += (InputListenner);
    setupReady = true;
  }

  private void InputListenner(char input)
  {
    if (input.Equals(definedWord[correctCount]))
    {
      transform.GetChild(correctCount).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KEYDown_PATH + definedWord[correctCount]);
      correctCount++;
    }

    if (correctCount == definedWord.Length) //typing complete
    {
      this.receiver.typingDataReceiver -= (InputListenner);
      Destroy(gameObject);
    };
  }

  private void MovementBehavior()
  {
    if (!(Vector3.Distance(transform.position, target.position) < 1f))
    {
      _PlayerDirection = Vector3.Normalize(new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y));
      transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * speed), transform.position.y - (_PlayerDirection.y * speed), 0.0f);
    }
  }
}