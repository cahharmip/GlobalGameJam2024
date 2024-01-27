using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
  [SerializeField]
  private PlayerController player;
  [SerializeField]
  private PlayerHUD playerHUD;
  [SerializeField]
  private TypingGameManager gameManager;
  [SerializeField]
  private ScoreUI scoreUI;
  [SerializeField]
  private ComboUI comboUI;

  private ScoreData scoreData;
  private ComboData comboData;
  private void Awake()
  {
    comboData = new ComboData();
    scoreData = new ScoreData(comboData);
    comboUI.Setup(comboData);
    scoreUI.Setup(scoreData);
    player.Setup();
    playerHUD.Setup();
    gameManager.Setup(scoreData, comboData);
  }
}
