using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingDataReceiver : MonoBehaviour
{
  [HideInInspector]
  public delegate void TypingDataReceiverDelegate(char data);
  public TypingDataReceiverDelegate typingDataReceiver;
  private string latestInput = default;
  private void Awake()
  {

  }

  private void Update()
  {
    latestInput = Input.inputString;
    if (!latestInput.Equals(string.Empty))
    {
      latestInput = latestInput.ToUpper();
      if (typingDataReceiver != null) typingDataReceiver(latestInput[0]);
    }
  }
}
