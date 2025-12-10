using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    [Header("Enemy Speed Settings")]
    public float minSpeed = 3f;  // velocidad mínima
    public float maxSpeed = 7f;  // velocidad máxima

    [Header("References")]
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // --- Velocidad aleatoria ---
        agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }

        // --- ANIMACIÓN ---
        if (animator != null)
        {
            bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
