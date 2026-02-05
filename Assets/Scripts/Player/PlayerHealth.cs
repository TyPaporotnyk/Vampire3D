using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = config.maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, config.maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, config.maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, config.maxHealth);

        if (CurrentHealth == 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, config.maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, config.maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("Player died");
    }
}
