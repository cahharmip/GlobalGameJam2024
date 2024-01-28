using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartMenu : MonoBehaviour
{
  private void Start()
  {
    FindObjectOfType<AudioManager>().Play("StartMenuBGM");
  }
}
