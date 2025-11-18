using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform Player;
    public GameObject EnemyPrefab;
    public GameObject BossPrefab;

    public float SpawnDistance = 10f;
    public float DistanceBetweenSpawns = 15f;

    private float NextSpawnX;
    private int EnemiesKilled = 0;
    private bool BossSpawned = false;   // <-- Para evitar 2 Bosses

    void Start()
    {
        NextSpawnX = Player.position.x + SpawnDistance;
    }

    void Update()
    {
        if (!BossSpawned && Player.position.x >= NextSpawnX)
        {
            SpawnEnemies();
            NextSpawnX += DistanceBetweenSpawns;
        }
    }

    void SpawnEnemies()
    {
        if (BossSpawned) return;

        int numEnemies = Random.Range(1, 4); // 1, 2 o 3 enemigos

        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 pos = new Vector3(Player.position.x + 8 + i * 1.5f, -1.5f, 0);
            Instantiate(EnemyPrefab, pos, Quaternion.identity);
        }
    }

    public void RegisterKill()
    {
        EnemiesKilled++;

        // Cuando se hayan muerto 20 enemigos → sale el Boss
        if (EnemiesKilled >= 20 && !BossSpawned)
        {
            SpawnBoss();
            BossSpawned = true;
        }
    }

    void SpawnBoss()
    {
        Vector3 pos = new Vector3(Player.position.x + 10f, -1.5f, 0);
        Instantiate(BossPrefab, pos, Quaternion.identity);
    }
}
