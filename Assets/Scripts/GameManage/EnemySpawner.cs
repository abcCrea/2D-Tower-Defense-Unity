using UnityEngine;
using TowerDefense.Enemies;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyData> enemyDataList;
    public Transform spawnPointsParent;
    public Transform endPointsParent;
    public GameData gameData;

    private float spawnRate = 0f;
    private Transform[] spawnPoints;
    private float timer;

    //Boolean for stop the spawn
    private bool isSpawning = true;

    public void StopSpawning()
    {
        isSpawning = false;
    }

    //Find the spawnPoints gameObject for spawn the Enemies
    void Start()
    {
        spawnRate = gameData.spawnRate;

        spawnPoints = System.Array.FindAll(
            spawnPointsParent.GetComponentsInChildren<Transform>(),
            t => t != spawnPointsParent
        );
    }

    //Loop for spawn enemies each spawnRate You can change the spawnRate on the GameData TD
    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    //Spawn a enemy on the random startPoint and define his endPoint
    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyDataList.Count == 0) return;

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Transform end = endPointsParent.Find(spawn.name);
        if (end == null)
        {
            Debug.LogWarning($"No matching endpoint found for spawn point '{spawn.name}'");
            return;
        }

        //Get a weight selected Enemy
        EnemyData data = GetRandomEnemyByWeight();
        if (data == null || data.prefab == null) return;

        //Create the gameObject enemy on the sccene
        GameObject enemy = Instantiate(data.prefab, spawn.position, Quaternion.identity);

        //Start the enemyController script
        var controller = enemy.GetComponent<EnemyController>();
        controller.Initialize(end);
    }

    //Function for select a Enemy for his Weight
    EnemyData GetRandomEnemyByWeight()
    {
        float totalWeight = 0f;
        foreach (var data in enemyDataList)
            totalWeight += data.weight;

        float roll = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var data in enemyDataList)
        {
            cumulative += data.weight;
            if (roll <= cumulative)
                return data;
        }

        return null;
    }
}
