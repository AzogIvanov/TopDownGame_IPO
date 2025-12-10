using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public int damage = 1; // cantidad de daño que hace

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si es un enemigo
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); // destruir bala al impactar
        }

        ExplosiveEnemyHealth explosiveEnemy = collision.GetComponent<ExplosiveEnemyHealth>();
        if (explosiveEnemy != null)
        {
            explosiveEnemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        BunkerHealth bunker = collision.GetComponent<BunkerHealth>();
        if (bunker != null)
        {
            bunker.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
