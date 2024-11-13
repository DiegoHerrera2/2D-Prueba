using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
    }

    [SerializeField] private int points = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerController>().OnCollectibleCollected.Invoke(points);
        Destroy(gameObject);
    }
}