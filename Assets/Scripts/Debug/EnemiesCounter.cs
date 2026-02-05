using UnityEngine;
using TMPro;


public class EnemiesCounter : MonoBehaviour
{
    private TextMeshProUGUI _enemiesCounter;
    [SerializeField] private EnemyController _controller;

    private void Awake()
    {
        _enemiesCounter = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _enemiesCounter.text = $"ENEMIES: {_controller.enemiesCount}";
    }
}
