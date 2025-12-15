using UnityEngine;
using System.Collections;

public class BunkerHealth : MonoBehaviour
{
    [HideInInspector]
    public bool isDestroyed = false;

    [Header("Health Settings")]
    public int health = 1;

    [Header("References")]
    public GameObject intactSprite;
    public GameObject destroyedSprite;
    public GameObject spawner;
    public ParticleSystem explosionFX;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip destroyExplosionClip;
    public float volumeExplosion = 0.3f;

    private Rigidbody2D rb;
    private Collider2D col;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (intactSprite != null)
        {
            intactSprite.SetActive(true);
            spriteRenderer = intactSprite.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                originalColor = spriteRenderer.color;
        }

        // Asegurarnos de que el sprite destruido empieza oculto
        if (destroyedSprite != null)
            destroyedSprite.SetActive(false);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        if (health <= 0)
        {
            DestroyBunker();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // dura 0.1 segundos, muy rápido
        spriteRenderer.color = originalColor;
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

        if (audioSource != null && destroyExplosionClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(destroyExplosionClip, volumeExplosion);
        }

        isDestroyed = true;
    }
}
