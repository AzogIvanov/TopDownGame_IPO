using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public bool isDead = false;

    [Header("Health")]
    public int health = 8;

    [Header("References")]
    public ParticleSystem bloodFX;
    public ParticleSystem toxicBloodFX;
    public ParticleSystem deadBloodExplosionFX;
    public ParticleSystem deadToxicBloodExplosionFX;

    [Header("Sprites")]
    public GameObject aliveSprite;
    public GameObject deadSprite;

    private Rigidbody2D rb;
    private Collider2D col;

    [Header("Settings")]
    public float destroyDelay = 7f;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (deadSprite != null)
            deadSprite.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;


        health -= dmg;

        // --- HIT FX ---
        if (bloodFX != null)
            bloodFX.Play();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        isDead = true;

        // Cambiar sprites
        if (aliveSprite != null)
            aliveSprite.SetActive(false);

        if (deadSprite != null)
            deadSprite.SetActive(true);

        // Desactivar Rigidbody y Collider
        if (rb != null)
            rb.simulated = false; // Rigidbody2D sigue existiendo pero no afecta físicas

        if (col != null)
            col.enabled = false;

        // --- Disable logic ---
        if (TryGetComponent(out EnemyController ec)) ec.enabled = false;
        if (TryGetComponent(out EnemyLookAtPlayer lp)) lp.enabled = false;
        if (TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;

        // DEATH FX
        if (deadBloodExplosionFX != null)
            deadBloodExplosionFX.Play();

        // --- Destroy body later ---
        Destroy(gameObject, destroyDelay);

    }
}
