using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private PlayerController _player;


    private void Awake() {
        _slider = GetComponent<Slider>();

        _player.OnHealthChanged += (currentHealth) => {
            _slider.value = currentHealth / 100f;
        };
    }
}
