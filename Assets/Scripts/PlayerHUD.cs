using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
  [SerializeField]
  private PlayerHPUI playerHPUI;
  public void Setup()
  {
    playerHPUI.Setup();
  }
}
