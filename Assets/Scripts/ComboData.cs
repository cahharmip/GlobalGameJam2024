using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum COMBO
{
  D = 0,
  C = 1,
  B = 2,
  A = 3,
  S = 4,
  SS = 5,
  SSS = 6,
}

public struct ComboStruct
{
  public int comboScore;
  public float scoreMultiplier;
  public string comboName;
}

public class ComboData
{
  private int currentScore = 0;
  private float scoreMultiplier = 1f;
  private string comboName = "";
  public delegate void OnComboUpdate(ComboStruct score);
  private OnComboUpdate comboUpdater;
  public ComboData()
  {
  }

  public void Subscribe(OnComboUpdate updater)
  {
    comboUpdater += updater;
  }

  public void UnSubscribe(OnComboUpdate updater)
  {
    comboUpdater -= updater;
  }

  public void UpdateScore(int score)
  {
    currentScore += score;
    if (currentScore <= 0) currentScore = 0;
    ComboStruct data = new ComboStruct();
    DoComboUpdate(currentScore);
    data.scoreMultiplier = this.scoreMultiplier;
    data.comboName = this.comboName;
    data.comboScore = currentScore;
    comboUpdater?.Invoke(data);
  }

  private void DoComboUpdate(int currentScore)
  {
    if (currentScore <= 10)
    {
      comboName = "Nicely \"D\"one";
      scoreMultiplier = 1.5f;
    }
    else if (currentScore <= 20)
    {
      comboName = "Cool!";
      scoreMultiplier = 2f;

    }
    else if (currentScore <= 40)
    {
      comboName = "Best!";
      scoreMultiplier = 3f;
    }
    else if (currentScore <= 60)
    {
      comboName = "Awesome!";
      scoreMultiplier = 4f;
    }
    else if (currentScore <= 90)
    {
      comboName = "SAVAGE!!";
      scoreMultiplier = 5f;
    }
    else if (currentScore <= 150)
    {
      comboName = "Sick Skills!!";
      scoreMultiplier = 6f;
    }
    else
    {
      comboName = "Smokin' Sexy Style!!";
      scoreMultiplier = 7f;
    }
  }

  public float GetScoreMultiplier()
  {
    return scoreMultiplier;
  }
}
