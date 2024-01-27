using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
  [SerializeField]
  private GameObject scoreUI;

  public void Setup(ScoreData scoreData)
  {
    scoreUI.GetComponent<TextMeshProUGUI>().text = "0";
    scoreData.Subscribe(OnScoreUpdate);
  }

  public void OnScoreUpdate(int score)
  {
    scoreUI.GetComponent<TextMeshProUGUI>().text = score.ToString();
  }
}
