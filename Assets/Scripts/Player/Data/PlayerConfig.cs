using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerConfig",
    menuName = "Configs/Player Config",
    order = 0)]
public class PlayerConfig : ScriptableObject
{
    [Header("Health")]
    public int maxHealth = 100;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundDrag = 6f;
    public float airDrag = 0f;
    public float groundDistance = 0.25f;
}