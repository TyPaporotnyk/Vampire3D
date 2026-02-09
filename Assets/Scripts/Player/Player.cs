using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.OnDied += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Player died");
    }
}
