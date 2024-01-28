using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFinalManager : MonoBehaviour
{
  public void Setup()
  {
    FindObjectOfType<AudioManager>().Stop("StartMenuBGM");
  }
}
