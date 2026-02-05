using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyBrain))]
public class Enemy : MonoBehaviour
{
    private EnemyMovement _movement;
    private EnemyBrain _brain;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _brain = GetComponent<EnemyBrain>();
    }

    public void MyFixedUpdate()
    {
        _movement.MyFixedUpdate();
        _brain.MyFixedUpdate();
    }
}