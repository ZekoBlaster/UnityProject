using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the inspector
    public float spawnInterval = 3f; // Time between each spawn
    AudioSource audioSource;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval); // Start spawning
        audioSource = GetComponent<AudioSource>();
    }

    void SpawnEnemy()
    {
        audioSource.Play();
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
