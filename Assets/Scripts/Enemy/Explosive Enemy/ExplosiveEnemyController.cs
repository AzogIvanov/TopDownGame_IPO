using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnemyController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    [Header("Enemy Speed Settings")]
    public float minSpeed = 3f;
    public float maxSpeed = 4f;

    [Header("References")]
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
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

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
