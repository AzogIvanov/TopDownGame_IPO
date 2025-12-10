using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 3f;
    private float lastAttackTime = 0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Time.time > lastAttackTime + attackCooldown)
            {
                PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastAttackTime = Time.time;
                    Debug.Log("Daño aplicado!");
                }
                else
                {
                    Debug.LogError("PlayerHealth no encontrado!");
                }
            }
        }
    }
}