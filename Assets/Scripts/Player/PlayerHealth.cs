using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector]
    public static bool isDead = false;

    [Header("References")]
    public GameObject aliveSprite;
    public GameObject deadSprite;
    private Color originalColor;

    public int maxHealth = 10;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip playerDeathClip;
    public float volumePlayerDeath = 0.3f;
    public AudioClip playerHurtClip;
    public float volumePlayerHurt = 0.3f;

    [HideInInspector]
    public int currentHealth;

    private Rigidbody2D rb;
    private Collider2D col;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (aliveSprite != null)
        {
            aliveSprite.SetActive(true);
            spriteRenderer = aliveSprite.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
                originalColor = spriteRenderer.color;
        }

        if (deadSprite != null)
            deadSprite.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (spriteRenderer != null)
            StartCoroutine(FlashRed());

        if (audioSource != null && playerHurtClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(playerHurtClip, volumePlayerHurt);
        }

        Debug.Log("Player Hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log("Player Dead");
        if (aliveSprite != null)
            aliveSprite.SetActive(false);

        if (deadSprite != null)
            deadSprite.SetActive(true);

        if (rb != null)
            rb.simulated = false;

        if (col != null)
            col.enabled = false;

        if (audioSource != null && playerDeathClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(playerDeathClip, volumePlayerDeath);
        }

        // --- Disable logic ---
        if (TryGetComponent(out PlayerShooting ps)) ps.enabled = false;

        isDead = true;
    }
}
