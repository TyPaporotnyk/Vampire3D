using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth health =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(config.damage);
        }
    }
}
