using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] Transform target;

    NavMeshAgent agent;

    Vector2 movement;

    [Header("References")]
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }

        // --- ANIMACIÓN ---
        if (animator != null)
        {
            // detecta si el navmesh agent se está moviendo
            bool isMoving = agent.velocity.sqrMagnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
