using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
  [SerializeField]
  private GameObject HPUI;

  private int maxHP;
  public void Setup()
  {
    maxHP = GameObject.Find("Player").GetComponent<PlayerController>().GetPlayerMaxHP();
    HPUI.GetComponent<TextMeshProUGUI>().text = ((GameObject.Find("Player").GetComponent<PlayerController>().GetPlayerCurrentHP() / maxHP)*100f).ToString();
    GameObject.Find("Player").GetComponent<PlayerController>().playerHpUpdater += OnHPUpdate;
  }

  public void OnHPUpdate(int currentHP)
  {
    HPUI.GetComponent<TextMeshProUGUI>().text = ((currentHP * 1f / maxHP) * 100).ToString();
  }
}
