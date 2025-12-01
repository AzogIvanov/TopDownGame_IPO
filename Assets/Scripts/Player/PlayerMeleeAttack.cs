using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;          // tamaño del área (editable)
    public LayerMask enemyLayers;           // capa de enemigos
    public int damage = 1;

    public float attackRate = 1f;           // ataques por segundo
    float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(1))   // clic derecho
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Detecta enemigos dentro del área
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Llamamos a un método TakeDamage() en cada enemigo
            //enemy.GetComponent<Enemy>()?.TakeDamage(damage);
        }

        // Aquí puedes meter animación de golpe o efecto visual
        Debug.Log("Ataque melee!");
    }

    // Para ver el área en el editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
