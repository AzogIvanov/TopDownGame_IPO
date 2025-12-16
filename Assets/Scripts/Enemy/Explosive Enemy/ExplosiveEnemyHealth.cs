using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public bool isDead = false;

    [Header("Health")]
    public int health = 8;

    [Header("Explosion Settings")]
    public float explosionRadius = 2.5f;
    public int explosionDamage = 2;

    [Header("References")]
    public ParticleSystem bloodFX;
    public ParticleSystem toxicBloodFX;
    public ParticleSystem deadBloodExplosionFX;
    public ParticleSystem deadToxicBloodExplosionFX;

    [Header("Sprites")]
    public GameObject aliveSprite;
    public GameObject deadSprite;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitZombieClip;
    public float volumeHitZombie = 0.3f;
    public AudioClip deathZombieClip;
    public float volumeDeathZombie = 0.3f;
    public AudioClip deathPuffZombieClip;
    public float volumeDeathPuffZombie = 0.3f;

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

        if (toxicBloodFX != null)
            toxicBloodFX.Play();

        if (audioSource != null && hitZombieClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(hitZombieClip, volumeHitZombie);
        }


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
        if (deadToxicBloodExplosionFX != null)
            deadToxicBloodExplosionFX.Play();

        if (audioSource != null && deathZombieClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(deathZombieClip, volumeDeathZombie);
        }

        if (audioSource != null && deathPuffZombieClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(deathPuffZombieClip, volumeDeathPuffZombie);
        }

        // --- Destroy body later ---
        Destroy(gameObject, destroyDelay);

        // --- EXPLSOIN ---
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerHealth ph = hit.GetComponent<PlayerHealth>();
                if (ph != null)
                    ph.TakeDamage(explosionDamage);
            }

            if (hit.CompareTag("Explosive Enemy"))
            {
                ExplosiveEnemyHealth eeh = hit.GetComponent<ExplosiveEnemyHealth>();
                if (eeh != null)
                    eeh.TakeDamage(explosionDamage);
            }

            if (hit.CompareTag("Zombie"))
            {
                EnemyHealth eh = hit.GetComponent<EnemyHealth>();
                if (eh != null)
                    eh.TakeDamage(explosionDamage);
            }
        }
        // --------

        Destroy(gameObject, destroyDelay);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

