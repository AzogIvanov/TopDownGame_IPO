using UnityEngine;

public class EnemyLookAtPlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador

    void Update()
    {
        if (player == null) return;

        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
