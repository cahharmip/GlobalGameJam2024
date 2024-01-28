using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuTypingPackage : MonoBehaviour
{
  private string definedWord = default;
  private int correctCount = 0;

  private bool setupReady = false;
  [SerializeField]
  private TypingDataReceiver receiver = default;
  public delegate void OnComplete();
  public event OnComplete onComplete;

  private void Awake()
  {
    definedWord = "START";
    receiver.typingDataReceiver += InputListenner;
    setupReady = true;
  }

  private void Update()
  {

  }

  private void FixedUpdate()
  {
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
      onComplete?.Invoke();
    };
  }

  public void CleanDestoy() //can't type in time
  {
    this.receiver.typingDataReceiver -= (InputListenner);
    Destroy(gameObject);
  }
}
