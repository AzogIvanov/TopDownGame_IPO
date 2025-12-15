using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public bool isDead = false;

    [Header("Health")]
    public int health = 1;

    [Header("References")]
    public ParticleSystem bloodFX;
    public ParticleSystem deadBloodExplosionFX;
    public ParticleSystem chargingToxicAttackFX;

    [Header("EXTERNAL OBJECTS")]
    public List<GameObject> disableOnDeath = new List<GameObject>();

    [Header("EXTERNAL PARTICLES")]
    public List<ParticleSystem> playParticlesOnDeath = new List<ParticleSystem>();

    [Header("Sprites")]
    public GameObject aliveSprite;
    public GameObject deadSprite;

    private Rigidbody2D rb;
    private Collider2D col;

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

        if (aliveSprite != null)
            aliveSprite.SetActive(false);

        if (deadSprite != null)
            deadSprite.SetActive(true);

        if (rb != null)
            rb.simulated = false;

        if (col != null)
            col.enabled = false;

        // --- Disable logic ---
        if (TryGetComponent(out BossEnemyAttack bea)) bea.enabled = false;
        if (TryGetComponent(out EnemyLookAtPlayer lp)) lp.enabled = false;

        // DEATH FX
        if (deadBloodExplosionFX != null)
            deadBloodExplosionFX.Play();

        if (chargingToxicAttackFX != null)
            chargingToxicAttackFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // ---- Disable Portals
        foreach (var go in disableOnDeath)
            if (go != null) go.SetActive(false);

        // ---- Play Particles
        foreach (var ps in playParticlesOnDeath)
            if (ps != null)
                ps.Play();

    }

}

