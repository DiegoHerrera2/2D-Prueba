using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
   [SerializeField] List<GameObject> DamagingObjects;
   [SerializeField] List<GameObject> Torchs;

   private bool isActive = false;

   void OnTriggerEnter2D(Collider2D other) {
    if (isActive) return;
    if (!other.gameObject.CompareTag("Player")) return;

    other.GetComponent<PlayerController>().TakeDamage(-10);

    foreach (var item in DamagingObjects)
    {
        item.SetActive(false);
    }

    foreach (var item in Torchs)
    {
        item.SetActive(true);
    }
    isActive = true;
   }
}
