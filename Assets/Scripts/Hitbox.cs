using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
  [SerializeField]
  private PlayerController playerController;
  protected void OnTriggerEnter2D(Collider2D collision)
  {
    TypingPackageController wordPackage = collision.GetComponent<TypingPackageController>();
    if(wordPackage != null)
    {
      playerController.UpdatePlayerHP(-wordPackage.GetDamage());
      Destroy(wordPackage.gameObject);
    }
  }
}
