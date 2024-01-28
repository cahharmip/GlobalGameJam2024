using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuTypingPackage : MonoBehaviour
{
  private string definedWord = default;
  private int correctCount = 0;

  [SerializeField]
  private TypingDataReceiver receiver = default;

  private void Awake()
  {
    definedWord = "START";
    receiver.typingDataReceiver += InputListenner;
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
      //Play outro animation and then loadScene
      Debug.Log("LoadScene");
      UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    };
  }

  public void CleanDestoy() //can't type in time
  {
    this.receiver.typingDataReceiver -= (InputListenner);
    Destroy(gameObject);
  }
}
