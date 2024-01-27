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
  private bool isSetup = false;
  private ScoreData scoreData;
  private ComboData comboData;
  private float sumTime;
  public void Setup(ScoreData scoreData, ComboData comboData)
  {
    this.scoreData = scoreData;
    this.comboData = comboData; 
    isSetup = true;
  }

  protected void FixedUpdate()
  {
    if (isSetup)
    {
      sumTime += Time.deltaTime;
      if (Mathf.FloorToInt(sumTime) > 0.5f)
      {
        comboData.UpdateScore(-1);
        sumTime = 0;
      }
      timeCounter += Time.deltaTime;
      if (wordIndex >= wordList.Length)
      {
        isSetup = false;
        return;
      }
      if (timeCounter >= TickTime)
      {
        timeCounter = 0;
        GameObject typingPackage = Instantiate(Resources.Load<GameObject>(Const.TYPING_PACKAGE_PATH));
        typingPackage.transform.SetParent(spawnPoint[(int)wordList[wordIndex].wpoint].transform);
        typingPackage.transform.localPosition = Vector3.zero;
        TypingDataReceiver receiver = GetComponent<TypingDataReceiver>();
        Transform player = GameObject.Find("Player").transform;
        typingPackage.GetComponent<TypingPackageController>().Setup(wordList[wordIndex], receiver, scoreData, comboData, player, () => { Destroy(typingPackage); });
        wordIndex++;
      }
    }
  }

  private void OnClearTypingPackage()
  {

  }
}
