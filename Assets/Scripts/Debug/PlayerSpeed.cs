using UnityEngine;
using TMPro;


public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    private TextMeshProUGUI _vSpeed;

    private void Awake()
    {
        _vSpeed = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Vector3 moveSpeed = _controller._moveDir * _controller.speed;

        _vSpeed.text = $"X: {moveSpeed.x} \nY: {moveSpeed.y} \nZ: {moveSpeed.z}";
    }
}
