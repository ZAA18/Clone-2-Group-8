 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Define enums here so both scripts can see them
public enum ArrowType
{
    Up,
    Down,
    Left,
    Right
}

public enum ArrowSpawnDirection
{
    Top,    // Moves down
    Bottom  // Moves up
}

public class Spawner : MonoBehaviour
{
    [Header("Arrow Prefabs - 4 Directions")]
    public GameObject arrowUpPrefab;
    public GameObject arrowDownPrefab;
    public GameObject arrowLeftPrefab;
    public GameObject arrowRightPrefab;
    
    [Header("Speed Settings")]
    public float startingSpeed = 2f;
    public float maxSpeed = 8f;
    public float speedIncreaseRate = 0.05f;
    
    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public float minSpawnInterval = 0.3f;
    public int arrowsPerSpawn = 2;
    
    [Header("Lane Settings")]
    public Transform centerLine;
    public float topY = 4f;
    public float bottomY = -4f;
    public float[] laneXPositions = { -3f, -1f, 1f, 3f };
    
    private float currentSpeed;
    private float currentSpawnInterval;
    private float gameTime = 0f;
    private System.Random random = new System.Random();
    private List<SpawnConfig> spawnConfigs = new List<SpawnConfig>();
    
    void Start()
    {
        currentSpeed = startingSpeed;
        currentSpawnInterval = spawnInterval;
        
        float centerY = centerLine.position.y;
        
        // Create all spawn configurations
        // TOP spawns (moving down)
        foreach (float x in laneXPositions)
        {
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, topY),
                centerY,
                ArrowSpawnDirection.Top,
                arrowUpPrefab,
                ArrowType.Up
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, topY),
                centerY,
                ArrowSpawnDirection.Top,
                arrowDownPrefab,
                ArrowType.Down
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, topY),
                centerY,
                ArrowSpawnDirection.Top,
                arrowLeftPrefab,
                ArrowType.Left
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, topY),
                centerY,
                ArrowSpawnDirection.Top,
                arrowRightPrefab,
                ArrowType.Right
            ));
        }
        
        // BOTTOM spawns (moving up)
        foreach (float x in laneXPositions)
        {
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, bottomY),
                centerY,
                ArrowSpawnDirection.Bottom,
                arrowUpPrefab,
                ArrowType.Up
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, bottomY),
                centerY,
                ArrowSpawnDirection.Bottom,
                arrowDownPrefab,
                ArrowType.Down
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, bottomY),
                centerY,
                ArrowSpawnDirection.Bottom,
                arrowLeftPrefab,
                ArrowType.Left
            ));
            
            spawnConfigs.Add(new SpawnConfig(
                new Vector2(x, bottomY),
                centerY,
                ArrowSpawnDirection.Bottom,
                arrowRightPrefab,
                ArrowType.Right
            ));
        }
        
        StartCoroutine(SpawnLoop());
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;
        
        // Increase speed over time
        currentSpeed = Mathf.Min(startingSpeed + (gameTime * speedIncreaseRate), maxSpeed);
        
        // Decrease spawn interval over time
        currentSpawnInterval = Mathf.Max(
            spawnInterval - (gameTime * 0.015f),
            minSpawnInterval
        );
    }
    
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            int count = random.Next(1, arrowsPerSpawn + 1);
            
            List<SpawnConfig> shuffled = new List<SpawnConfig>(spawnConfigs);
            ShuffleList(shuffled);
            
            for (int i = 0; i < count && i < shuffled.Count; i++)
            {
                SpawnArrow(shuffled[i]);
            }
            
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }
    
    void SpawnArrow(SpawnConfig config)
    {
        GameObject arrow = Instantiate(config.prefab, config.startPos, Quaternion.identity);
        
        ArrowController controller = arrow.GetComponent<ArrowController>();
        if (controller != null)
        {
            controller.speed = currentSpeed;
            controller.targetY = config.targetY;
            controller.spawnDirection = config.spawnDirection;
            controller.arrowType = config.arrowType;
        }
    }
    
    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = random.Next(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), $"Speed: {currentSpeed:F1}");
        GUI.Label(new Rect(10, 40, 200, 30), $"Spawn Rate: {currentSpawnInterval:F2}s");
        GUI.Label(new Rect(10, 70, 200, 30), $"Time: {gameTime:F0}s");
    }
}

[System.Serializable]
public class SpawnConfig
{
    public Vector2 startPos;
    public float targetY;
    public ArrowSpawnDirection spawnDirection;
    public GameObject prefab;
    public ArrowType arrowType;
    
    public SpawnConfig(Vector2 start, float target, ArrowSpawnDirection dir, GameObject prefab, ArrowType type)
    {
        this.startPos = start;
        this.targetY = target;
        this.spawnDirection = dir;
        this.prefab = prefab;
        this.arrowType = type;
    }
}
