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
        Vector3 moveDir = _controller._moveDir;

        _vSpeed.text = $"X: {moveDir.x} \nY: {moveDir.y} \nZ: {moveDir.z}";
    }
}
