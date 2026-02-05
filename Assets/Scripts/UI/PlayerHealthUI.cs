using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        health.OnHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(int current, int max)
    {
        slider.value = (float)current / max;
    }
}
