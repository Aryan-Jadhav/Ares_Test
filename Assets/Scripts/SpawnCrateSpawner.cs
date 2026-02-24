using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private SpawnCrateData spawnConfig;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        for (int i = 0; i < spawnConfig.prefabs.Count; i++)
        {
            if (i >= spawnPoints.Length) break;

            Instantiate(
                spawnConfig.prefabs[i],
                spawnPoints[i].position,
                Quaternion.identity
            );
        }
    }
}