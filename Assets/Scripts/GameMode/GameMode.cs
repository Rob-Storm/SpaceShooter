using System;
using System.Collections;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[Serializable]
public class GameMode : MonoBehaviour
{
    public static Difficulty Difficulty { get; private set; } = Difficulty.Easy;

    private PlayerHealth playerHealth;

    [SerializeField] private float spawnTimer;
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float minSpawnVectorX, maxSpawnVectorX;
    private int difficultyTimer = 0;
    private bool canSpawn = true;
    private Unity.Mathematics.Random random;

    private const int EASY_DIFFICULTY_TIMER = 0, MEDIUM_DIFFICULTY_TIMER = 20, HARD_DIFFICULTY_TIMER = 50;

    private const float EASY_DIFFICULTY_SPAWN_TIMER = 2.0f, MEDIUM_DIFFICULTY_SPAWN_TIMER = 1.5f, HARD_DIFFICULTY_SPAWN_TIMER = 0.75f;

    private void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();

        playerHealth.OnPlayerDie += OnPlayerDie;
    }

    private void OnPlayerDie(object sender, EventArgs e)
    {
        canSpawn = false;
        difficultyTimer = -1;
        AddDifficulty();
        Debug.Log("Player be dead!");
    }

    private void Start()
    {
        random = new Unity.Mathematics.Random((uint)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

        DifficultyChanged();

        StartCoroutine(SpawnAsteroid());
    }


    private void AddDifficulty()
    {
        difficultyTimer++;

        switch(difficultyTimer) 
        {
            case EASY_DIFFICULTY_TIMER:
                Difficulty = Difficulty.Easy; break;

            case MEDIUM_DIFFICULTY_TIMER:
                Difficulty = Difficulty.Medium; break;

            case HARD_DIFFICULTY_TIMER:
                Difficulty = Difficulty.Hard; break;
        }

        DifficultyChanged();
    }

    private void DifficultyChanged()
    {
        switch(Difficulty)
        {
            case Difficulty.Easy: 
                spawnTimer = EASY_DIFFICULTY_SPAWN_TIMER; break;

            case Difficulty.Medium: 
                spawnTimer = MEDIUM_DIFFICULTY_SPAWN_TIMER;  break;

            case Difficulty.Hard: 
                spawnTimer = HARD_DIFFICULTY_SPAWN_TIMER;  break;
        }

        Debug.Log(Difficulty);
    }

    private IEnumerator SpawnAsteroid()
    {
        if(canSpawn)
        {
            Instantiate(asteroid, new Vector3(random.NextFloat(minSpawnVectorX, maxSpawnVectorX), 8.5f, 0f), Quaternion.identity);
        }

        Debug.Log("Spawned Asteroid");

        AddDifficulty();

        yield return new WaitForSeconds(spawnTimer);

        StartCoroutine(SpawnAsteroid());
    }

    ~GameMode()
    {
        playerHealth.OnPlayerDie -= OnPlayerDie;
        StopAllCoroutines();
    }

}
