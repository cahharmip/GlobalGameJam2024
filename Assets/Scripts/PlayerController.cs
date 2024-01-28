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
  [SerializeField]
  private float healPower = 0.1f;
  [SerializeField]
  private WordObject healWord;
  [SerializeField]
  private Transform healWordPosition;
  [SerializeField]
  private HealPackageController healPackage;
  [SerializeField]
  private TypingDataReceiver typingDataReceiver;
  [SerializeField]
  private Animator animator;

  public void Setup()
  {
    playerCurrentHP = playerMaxHP;
    SetupHealText();
    if (typingDataReceiver != null)
    {
      typingDataReceiver.typingDataReceiver += OnGetInput;
    }
  }

  public int GetPlayerCurrentHP()
  {
    return playerCurrentHP;
  }

  public void UpdatePlayerHP(int value)
  {
    playerCurrentHP += value;
    if (playerCurrentHP > playerMaxHP)
    {
      playerCurrentHP = playerMaxHP;
    }
    playerHpUpdater?.Invoke(playerCurrentHP);

    if (playerCurrentHP <= 0)
    {
      //GameEnd
    }
  }

  public int GetPlayerMaxHP()
  {
    return playerMaxHP;
  }

  private void SetupHealText()
  {
    healPackage.Setup(healWord, GameObject.Find("TypingGameController").GetComponent<TypingDataReceiver>(), null, () => { Heal(); healPackage.ResetState(); });
  }

  private void Heal()
  {
    UpdatePlayerHP(Mathf.CeilToInt(playerCurrentHP * healPower));
  }

  private void OnGetInput(char input)
  {
    //Debug.Log("player get input -> " + input);
    animator.SetTrigger("NextAnimation");
  }

  private void OnDestroy()
  {
    typingDataReceiver.typingDataReceiver -= OnGetInput;
  }
}
