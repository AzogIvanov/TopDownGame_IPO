using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zombie Settings")]
    public GameObject zombiePrefab;
    public Transform player;
    public int maxZombies = 10;
    public float spawnInterval = 2f;

    [Header("Spawn Area")]
    public Vector2 areaSize = new Vector2(10f, 10f); // ancho/alto del área
    public LayerMask obstacleMask; // evita paredes

    private float nextSpawnTime = 0f;


    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawnZombie();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void TrySpawnZombie()
    {
        // No spawnear si ya hay muchos
        if (GameObject.FindGameObjectsWithTag("Zombie").Length >= maxZombies)
            return;

        Vector2 spawnPos = GetRandomPosition();

        // OPCIONAL: evitar colisiones con obstáculos
        if (Physics2D.OverlapCircle(spawnPos, 0.5f, obstacleMask) != null)
            return; // intenta más tarde

        // Instanciar
        GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);

        // Asignar player automáticamente
        var controller = zombie.GetComponent<EnemyController>();
        if (controller != null) controller.SetTarget(player);

        var look = zombie.GetComponent<EnemyLookAtPlayer>();
        if (look != null) look.SetPlayer(player);
    }

    Vector2 GetRandomPosition()
    {
        Vector2 center = transform.position;

        float x = Random.Range(center.x - areaSize.x / 2f, center.x + areaSize.x / 2f);
        float y = Random.Range(center.y - areaSize.y / 2f, center.y + areaSize.y / 2f);

        return new Vector2(x, y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
        Gizmos.DrawCube(transform.position, areaSize);
    }
}
