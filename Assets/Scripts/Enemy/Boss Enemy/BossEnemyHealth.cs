using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

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
    public BossEnemyHealth bossEnemyHealth;

    [Header("Boss Sounds")]
    public AudioSource audioSource;
    public AudioClip[] bossSounds;
    public float minWaitTime = 2f;
    public float maxWaitTime = 4f;
    public float minPitch = 0.8f;
    public float maxPitch = 1.3f;
    public float minVolume = 0.6f;
    public float maxVolume = 1f;

    [Header("Audio")]
    public AudioSource audioSource2;
    public AudioClip hitBossClip;
    public float volumeHitBoss = 0.3f;
    public AudioClip deathBossClip;
    public float volumeDeathBoss = 0.3f;

    [Header("Audio Portals")]
    public AudioSource audioSourcePortal1;
    public AudioSource audioSourcePortal2; 
    public AudioClip deathPuffPortalClip;
    public float volumePuffPortalClip = 0.3f;


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

        if (audioSource != null && bossSounds.Length > 0)
            StartCoroutine(ZombieSoundLoop());
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;


        health -= dmg;

        // --- HIT FX ---
        if (bloodFX != null)
            bloodFX.Play();

        if (audioSource != null && hitBossClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(hitBossClip, volumeHitBoss);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    IEnumerator ZombieSoundLoop()
    {
        while (!bossEnemyHealth.isDead)
        {
            float wait = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(wait);

            if (bossEnemyHealth.isDead) break;

            AudioClip clip = bossSounds[Random.Range(0, bossSounds.Length)];

            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = Random.Range(minVolume, maxVolume);

            audioSource.clip = clip;
            audioSource.Play();

            yield return new WaitForSeconds(clip.length);
        }

        audioSource.Stop();
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

        if (audioSource != null && deathBossClip != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(deathBossClip, volumeDeathBoss);
        }

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

