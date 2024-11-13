using UnityEngine;

public class Treasure : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Time.timeScale = 0;
    }
}
