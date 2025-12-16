using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class ExplosiveEnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    [Header("Enemy Speed Settings")]
    public float minSpeed = 3f;
    public float maxSpeed = 4f;

    [Header("References")]
    public Animator animator;
    public ExplosiveEnemyHealth explosiveEnemyHealth;

    [Header("Explsive Enemy Sounds")]
    public AudioSource audioSource;
    public AudioClip[] zombieSounds;
    public float minWaitTime = 2f;
    public float maxWaitTime = 4f;
    public float minPitch = 0.8f;
    public float maxPitch = 1.3f;
    public float minVolume = 0.6f;
    public float maxVolume = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = Random.Range(minSpeed, maxSpeed);

        if (audioSource != null && zombieSounds.Length > 0)
            StartCoroutine(ZombieSoundLoop());
    }

    void Update()
    {
        if (explosiveEnemyHealth.isDead == true)
        {
            audioSource.Stop();
        }

        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }

        if (animator != null)
        {
            bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    IEnumerator ZombieSoundLoop()
    {
        while (!explosiveEnemyHealth.isDead) // se detiene si el enemigo muere
        {
            // tiempo aleatorio entre gruñidos
            float wait = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(wait);

            if (explosiveEnemyHealth.isDead) break; // comprobación extra por si muere durante la espera

            // sonido aleatorio
            AudioClip clip = zombieSounds[Random.Range(0, zombieSounds.Length)];

            // pitch y volumen aleatorio
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = Random.Range(minVolume, maxVolume);

            // reproducir clip directamente
            audioSource.clip = clip;
            audioSource.Play();

            // esperar a que termine el sonido antes de pasar al siguiente
            yield return new WaitForSeconds(clip.length);
        }

        // al morir, detener cualquier sonido que estuviera reproduciéndose
        audioSource.Stop();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
