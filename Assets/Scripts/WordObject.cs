using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Word", menuName = "Create Word")]
public class WordObject : ScriptableObject
{
  public enum SPANWPOINT
  {
    Bot = 0,
    MID = 1,
    HIGH = 2,
    RANDOM = 3,
  }

  public string word = default;
  public float durationToTarget = default;
  public SPANWPOINT wpoint = default;
  public int damage = 1;
  public float delayNextWord = 0f;
}
