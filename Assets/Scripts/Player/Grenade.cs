using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 2.5f;
    public int explosionDamage = 3;
    public float lifeTime = 1.5f;
    public ParticleSystem explosionFX;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public float explosionVolume = 0.3f;

    private void Start()
    {
        // Auto-explota tras X segundos si no choca
        Invoke(nameof(Explode), lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    void Explode()
    {
        // Efecto visual
        if (explosionFX != null)
        {
            ParticleSystem fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            fx.Play();
            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
        }

        if (explosionSound != null)
        {
            GameObject tempAudio = new GameObject("TempExplosionSound");
            tempAudio.transform.position = transform.position;
            AudioSource source = tempAudio.AddComponent<AudioSource>();
            source.clip = explosionSound;
            source.volume = explosionVolume;
            source.Play();
            Destroy(tempAudio, explosionSound.length);
        }

        // Detección
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var h in hits)
        {
            if (h.CompareTag("Zombie"))
            {
                EnemyHealth eh = h.GetComponent<EnemyHealth>();
                if (eh != null) eh.TakeDamage(explosionDamage);
            }

            if (h.CompareTag("Explosive Enemy"))
            {
                ExplosiveEnemyHealth eeh = h.GetComponent<ExplosiveEnemyHealth>();
                if (eeh != null) eeh.TakeDamage(explosionDamage);
            }

            if (h.CompareTag("Boss"))
            {
                BossEnemyHealth beh = h.GetComponent<BossEnemyHealth>();
                if (beh != null) beh.TakeDamage(explosionDamage);
            }

            if (h.CompareTag("Player"))
            {
                PlayerHealth ph = h.GetComponent<PlayerHealth>();
                if (ph != null) ph.TakeDamage(explosionDamage / 2);
            }

            if (h.CompareTag("Bunker"))
            {
                BunkerHealth ph = h.GetComponent<BunkerHealth>();
                if (ph != null) ph.TakeDamage(explosionDamage / 2);
            }

            if (h.CompareTag("GooTank"))
            {
                GooTank ph = h.GetComponent<GooTank>();
                if (ph != null) ph.TakeDamage(explosionDamage / 2);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
