using UnityEngine;
using System.Collections.Generic;

public class LevelCoordinator : MonoBehaviour
{
    private List<SpawnCrateSpawner> spawnSpawners;
    private List<AcceptingCrateSpawner> acceptingSpawners;

    private List<CardColor> collectedColors = new List<CardColor>();
    private int reportedSpawners = 0;

    private void Awake()
    {
        spawnSpawners = new List<SpawnCrateSpawner>(FindObjectsByType<SpawnCrateSpawner>(FindObjectsSortMode.None));

        acceptingSpawners = new List<AcceptingCrateSpawner>(FindObjectsByType<AcceptingCrateSpawner>(FindObjectsSortMode.None));
    }

    public void ReportColors(List<CardColor> colorsFromCards)
    {
        collectedColors.AddRange(colorsFromCards);
        reportedSpawners++;

        // Wait until ALL spawn spawners have reported
        if (reportedSpawners >= spawnSpawners.Count)
        {
            DistributeAcceptingCrates();
        }
    }

    private void DistributeAcceptingCrates()
    {
        if (acceptingSpawners.Count == 0) return;

        int spawnerIndex = 0;

        foreach (CardColor color in collectedColors)
        {
            AcceptingCrateSpawner target = acceptingSpawners[spawnerIndex];
            target.SpawnSingleAcceptingCrate(color);

            spawnerIndex++;

            if (spawnerIndex >= acceptingSpawners.Count)
                spawnerIndex = 0;
        }
    }
}