using UnityEngine;

public class MultiEnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemyPrefab;
        [Range(0f, 1f)]
        public float spawnChance = 1f; // probabilidad de aparición
    }

    [Header("Enemy Types")]
    public EnemyData[] enemyTypes;

    [Header("General Settings")]
    public Transform player;
    public float spawnInterval = 2f;
    public int maxEnemies = 20;

    [Header("Spawn Area")]
    public Vector2 areaSize = new Vector2(10f, 10f);

    private float nextSpawnTime = 0f;


    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            TrySpawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }


    void TrySpawn()
    {
        // Evita sobrepoblación
        int currentEnemies =
            GameObject.FindGameObjectsWithTag("Zombie").Length +
            GameObject.FindGameObjectsWithTag("Explosive Enemy").Length;

        if (currentEnemies >= maxEnemies)
            return;

        // Elegir tipo según probabilidad
        GameObject selectedEnemy = ChooseRandomEnemy();
        if (selectedEnemy == null) return;

        // Spawnear
        Vector2 spawnPos = GetRandomPosition();
        GameObject enemy = Instantiate(selectedEnemy, spawnPos, Quaternion.identity);

        // Asignar target si existe
        var controller = enemy.GetComponent<EnemyController>();
        if (controller != null) controller.SetTarget(player);

        var controller2 = enemy.GetComponent<ExplosiveEnemyController>();
        if (controller2 != null) controller2.SetTarget(player);

        var look = enemy.GetComponent<EnemyLookAtPlayer>();
        if (look != null) look.SetPlayer(player);
    }


    GameObject ChooseRandomEnemy()
    {
        float totalChance = 0f;
        foreach (var e in enemyTypes)
            totalChance += e.spawnChance;

        float randomValue = Random.Range(0f, totalChance);
        float current = 0f;

        foreach (var e in enemyTypes)
        {
            current += e.spawnChance;
            if (randomValue <= current)
                return e.enemyPrefab;
        }

        return null;
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
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawCube(transform.position, areaSize);
    }
}
