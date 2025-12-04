using UnityEngine;

public class BunkerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int health = 1;

    [Header("References")]
    public GameObject intactSprite;     // hijo con sprite original
    public GameObject destroyedSprite;  // hijo con sprite destruido
    public GameObject spawner;          // hijo con spawner de zombies
    public ParticleSystem explosionFX;

    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Asegurarnos de que el sprite destruido empieza oculto
        if (destroyedSprite != null)
            destroyedSprite.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            DestroyBunker();
        }
    }

    void DestroyBunker()
    {
        // Cambiar sprites
        if (intactSprite != null)
            intactSprite.SetActive(false);

        if (destroyedSprite != null)
            destroyedSprite.SetActive(true);

        // Desactivar spawner
        if (spawner != null)
            spawner.SetActive(false);

        // Desactivar Rigidbody y Collider
        if (rb != null)
            rb.simulated = false; // Rigidbody2D sigue existiendo pero no afecta físicas

        if (col != null)
            col.enabled = false;

        // DESTROY FX
        if (explosionFX != null)
            explosionFX.Play();
    }
}
