using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator _animator;
    
    private AudioSource _source;
    public Action onHit;

    private int _health = 3;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
        onHit += () => { 
            _animator.SetTrigger("onHit"); 
            _health -= 1;
            if (_health == 0) {
                _source.Play();
                Destroy(gameObject, 0.8f);
            }
        };
    }
}
