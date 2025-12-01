using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1; // muere con 1 impacto

    ParticleSystem bloodFX;


    private void Start()
    {
        //bloodFX = GetComponentInChildren<ParticleSystem>();
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        // BLOOD FX
        //if (bloodFX != null)
        //    bloodFX.Play();

        if (health <= 0)
        {
            Die();
        }
    }



    void Die()
    {
        Destroy(gameObject);
    }
}
