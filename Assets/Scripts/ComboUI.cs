using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
  [SerializeField]
  private GameObject comboUI;

  public void Setup(ComboData comboData)
  {
    comboUI.GetComponent<TextMeshProUGUI>().text = "0";
    comboData.Subscribe(OnScoreUpdate);
  }

  public void OnScoreUpdate(ComboStruct combo)
  {
    comboUI.GetComponent<TextMeshProUGUI>().text = (combo.comboName.ToString() + "\n" + combo.comboScore.ToString());
  }
}
