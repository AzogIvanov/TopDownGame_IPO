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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
