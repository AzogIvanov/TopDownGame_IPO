using UnityEngine;

public class BossEnemyAttack : MonoBehaviour
{
    [Header("References")]
    public Transform firePoint;
    public GameObject acidProjectilePrefab;
    public Transform target;

    [Header("Attack Settings")]
    public float fireCooldown = 2f;
    public float projectileSpeed = 4f;

    private float lastFireTime = 0f;

    void Update()
    {
        if (target == null) return;

        if (Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector2 direction = (target.position - firePoint.position).normalized;

        GameObject projectile = Instantiate(
            acidProjectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
