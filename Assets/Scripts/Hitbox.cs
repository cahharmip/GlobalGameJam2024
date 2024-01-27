using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
  protected void OnTriggerEnter2D(Collider2D collision)
  {
    TypingPackageController wordPackage = collision.GetComponent<TypingPackageController>();
    if(wordPackage != null)
    {
      //reduce player health based on word config.
      Destroy(wordPackage.gameObject);
    }
  }
}
