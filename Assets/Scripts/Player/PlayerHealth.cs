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

        Debug.Log("Player Hit! Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // dura 0.1 segundos, muy rápido
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log("Player Dead");
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
        if (TryGetComponent(out PlayerShooting ps)) ps.enabled = false;

        isDead = true;
    }
}
