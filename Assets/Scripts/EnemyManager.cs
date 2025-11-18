using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform Player;
    public GameObject EnemyPrefab;
    public GameObject BossPrefab;

    public float SpawnDistance = 10f;        
    public float DistanceBetweenSpawns = 15f;
    public float EnemySpawnY = -2.7f;

    private float NextSpawnX;
    private int EnemiesKilled = 0;
    private bool BossSpawned = false;

    void Start()
    {
        NextSpawnX = Player.position.x + SpawnDistance;
    }

    void Update()
    {
        if (Player == null) return;

        // Si el jugador ya pasó el punto de spawn → generar enemigos
        if (!BossSpawned && Player.position.x >= NextSpawnX)
        {
            SpawnEnemies();
            NextSpawnX += DistanceBetweenSpawns;
        }
    }

    void SpawnEnemies()
    {
        if (BossSpawned) return;

        int numEnemies = Random.Range(1, 4);

        for (int i = 0; i < numEnemies; i++)
        {
            float spawnX = Player.position.x + SpawnDistance + i * 2f;
            Vector3 spawnPos = new Vector3(spawnX, EnemySpawnY, 0);
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void RegisterKill()
    {
        EnemiesKilled++;

        // Cuando se hayan muerto 20 enemigos → aparece el Boss
        if (EnemiesKilled >= 20 && !BossSpawned)
        {
            SpawnBoss();
            BossSpawned = true;
        }
    }

    void SpawnBoss()
    {
        Vector3 pos = new Vector3(Player.position.x + 10f, EnemySpawnY, 0);
        Instantiate(BossPrefab, pos, Quaternion.identity);
    }
}
