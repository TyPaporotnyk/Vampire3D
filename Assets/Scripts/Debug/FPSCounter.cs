using UnityEngine;
using TMPro;


public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _fpsCounter;
    private float _deltaTime;

    private void Awake()
    {
        _fpsCounter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1f / _deltaTime;

        _fpsCounter.text = $"FPS: {Mathf.Round(fps)}";
    }
}
