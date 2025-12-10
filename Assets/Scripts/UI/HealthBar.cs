using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    private PlayerHealth player;
    private float maxHealth;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        maxHealth = player.maxHealth;
    }

    void Update()
    {
        healthBarFill.fillAmount = player.currentHealth / maxHealth;
    }
}
