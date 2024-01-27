using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
  [SerializeField]
  private PlayerController player;
  [SerializeField]
  private PlayerHUD playerHUD;
  private void Awake()
  {
    player.Setup();
    playerHUD.Setup();
  }
}
