using UnityEngine;
using TMPro;


public class EnemiesCounter : MonoBehaviour
{
    private TextMeshProUGUI _enemiesCounter;
    private float _deltaTime;

    private void Awake()
    {
        _enemiesCounter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        int enemiesCount = 0;
        _enemiesCounter.text = $"ENEMIES: {enemiesCount}";
    }
}
