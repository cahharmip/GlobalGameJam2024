using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private int playerMaxHP = 10;

  private int playerCurrentHP = 0;

  public delegate void PlayerHP(int updateValue);
  public PlayerHP playerHpUpdater;
  public void Setup()
  {
    playerCurrentHP = playerMaxHP;
  }

  public int GetPlayerCurrentHP()
  {
    return playerCurrentHP;
  }

  public void UpdatePlayerHP(int value)
  {
    playerCurrentHP += value;
    playerHpUpdater?.Invoke(playerCurrentHP);
  }

  public int GetPlayerMaxHP()
  {
    return playerMaxHP;
  }
}
