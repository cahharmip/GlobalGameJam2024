using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxScaler : MonoBehaviour
{
  public TextMeshProUGUI dialogueText;
  public RectTransform dialogueBoxRectTransform;
  public Vector2 padding;

  private Coroutine typingCoroutine;

  private void Start()
  {
    UpdateDialogue("Welcome to the game!");
  }

  public void UpdateDialogue(string newDialogue)
  {
    if (typingCoroutine != null)
    {
      StopCoroutine(typingCoroutine);
    }
    typingCoroutine = StartCoroutine(TypeDialogue(newDialogue));
  }

  IEnumerator TypeDialogue(string dialogue)
  {
    dialogueText.text = ""; // Clear current text
    foreach (char letter in dialogue.ToCharArray())
    {
      dialogueText.text += letter;
      AdjustSizeToFitText(); // Adjust the box size after each character is added
      yield return new WaitForSeconds(0.05f); // Wait a bit before adding the next character
    }
  }

  void AdjustSizeToFitText()
  {
    // Calculate the size of the text content plus padding
    Vector2 textSize = dialogueText.GetPreferredValues(dialogueText.text) + padding;

    // Adjust the size of the dialogue box, making it able to shrink if the text is smaller than the box size
    dialogueBoxRectTransform.sizeDelta = new Vector2(
        textSize.x,
        textSize.y);
  }
}
