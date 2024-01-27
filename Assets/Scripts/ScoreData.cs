using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
  private int currentScore = 0;
  public delegate void OnScoreUpdate(int score);
  private OnScoreUpdate scoreUpdater;
  private ComboData comboData;
  public ScoreData(ComboData comboData)
  {
    this.comboData = comboData;
  }

  public void Subscribe(OnScoreUpdate updater)
  {
    scoreUpdater += updater;
  }

  public void UnSubscribe(OnScoreUpdate updater)
  {
    scoreUpdater -= updater;
  }

  public void UpdateScore(int score)
  {
    currentScore += Mathf.CeilToInt(score * comboData.GetScoreMultiplier());
    scoreUpdater?.Invoke(currentScore);
  }

}
