using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGameManager : MonoBehaviour
{
  [SerializeField]
  private WordObject[] wordList = default;
  [SerializeField]
  private Transform[] spawnPoint = default;
  [SerializeField]
  private float TickTime = 0f;

  private float timeCounter = 0;
  private int wordIndex = 0;
  private string TYPING_PACKAGE_PATH = "Prefab/TypingPackage";
  protected void Awake()
  {

  }

  protected void FixedUpdate()
  {
    timeCounter += Time.deltaTime;
    if (wordIndex >= wordList.Length) return;
    if (timeCounter >= TickTime)
    {
      timeCounter = 0;
      GameObject typingPackage = Instantiate(Resources.Load<GameObject>(TYPING_PACKAGE_PATH));
      typingPackage.transform.SetParent(spawnPoint[(int) wordList[wordIndex].wpoint].transform);
      typingPackage.transform.localPosition = Vector3.zero;
      TypingDataReceiver receiver = GetComponent<TypingDataReceiver>();
      Transform player = GameObject.Find("Player").transform;
      typingPackage.GetComponent<TypingPackageController>().Setup(wordList[wordIndex], receiver, player);
      wordIndex++;
    }
  }
}
