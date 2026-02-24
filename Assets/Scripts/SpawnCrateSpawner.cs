using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] private SpawnCrateData spawnConfig;
    [SerializeField] private float spacing = 2f;

    private void Start()
    {
        SpawnPrefabs();
    }

    private void SpawnPrefabs()
    {
        for (int i = 0; i < spawnConfig.prefabs.Count; i++)
        {
            GameObject prefab = spawnConfig.prefabs[i];

            // Rotation logic
            Vector3 prefabEuler = prefab.transform.rotation.eulerAngles;
            Vector3 spawnEuler = transform.rotation.eulerAngles;

            Quaternion finalRotation = Quaternion.Euler(
                prefabEuler.x,
                spawnEuler.y,
                prefabEuler.z
            );

            // Offset along spawn point's right direction
            Vector3 offset = transform.forward * i * spacing;

            GameObject obj = Instantiate(
                prefab,
                transform.position + offset,
                finalRotation
            );

            obj.transform.SetParent(transform);
        }

    }
}