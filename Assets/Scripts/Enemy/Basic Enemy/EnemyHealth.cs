using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public bool isDead = false;

    [Header("Explosion")]
    public float explosionRadius = 2f;
    public int explosionDamage = 3;
    public LayerMask damageLayer;

    [Header("Health")]
    public int health = 1;

    [Header("References")]
    public ParticleSystem bloodFX;
    public ParticleSystem deadBloodExplosionFX;

    [Header("Sprites")]
    public GameObject aliveSprite;
    public GameObject deadSprite;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitZombieClip;
    public float volumeHitZombie = 0.3f;
    public AudioClip deathZombieClip;
    public float volumeDeathZombie = 0.3f;


    private Rigidbody2D rb;
    private Collider2D col;

    [Header("Settings")]
    public float destroyDelay = 5f;

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

        if (bloodFX != null)
            bloodFX.Play();

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

        if (audioSource != null && deathZombieClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(deathZombieClip, volumeDeathZombie);
        }

        // --- Destroy body later ---
        Destroy(gameObject, destroyDelay);

    }
    
}
