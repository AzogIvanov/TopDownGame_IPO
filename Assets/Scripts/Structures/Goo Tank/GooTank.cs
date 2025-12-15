using UnityEngine;
using System.Collections;

public class GooTank : MonoBehaviour
{
    [HideInInspector]
    public bool isDestroyed = false;

    [Header("Health Settings")]
    public int health = 1;

    [Header("References")]
    public GameObject intactSprite;
    public GameObject destroyedSprite;
    public ParticleSystem toxicExplosionFX;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip breakingGlassClip;
    public float volumeBreakingGlass = 0.2f;

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
        if (isDestroyed == false)
        {
            health -= dmg;

            if (health <= 0)
            {
                DestroyTank();
            }
        }

    }

    void DestroyTank()
    {
        // Cambiar sprites
        if (intactSprite != null)
            intactSprite.SetActive(false);

        if (destroyedSprite != null)
            destroyedSprite.SetActive(true);

        // DESTROY FX
        if (toxicExplosionFX != null)
            toxicExplosionFX.Play();

        if (audioSource != null && breakingGlassClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(breakingGlassClip, volumeBreakingGlass);
        }

        isDestroyed = true;
    }
}
