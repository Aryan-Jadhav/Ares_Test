using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrateSpawner : MonoBehaviour
{
    [SerializeField] private SpawnCrateData spawnConfig;
    [SerializeField] private float spacing = 2f;

    [SerializeField] private LevelCoordinator levelCoordinator;

    private List<Transform> spawnedCrates = new List<Transform>();

    private IEnumerator Start()
    {
        SpawnPrefabs();

        yield return null; // wait for CardSpawner to set color

        ReportColors();
    }

    private void SpawnPrefabs()
    {
        for (int i = 0; i < spawnConfig.prefabs.Count; i++)
        {
            GameObject prefab = spawnConfig.prefabs[i];

            Vector3 prefabEuler = prefab.transform.rotation.eulerAngles;
            Vector3 spawnEuler = transform.rotation.eulerAngles;

            Quaternion finalRotation = Quaternion.Euler(
                prefabEuler.x,
                spawnEuler.y,
                prefabEuler.z
            );

            Vector3 offset = transform.up * i * spacing;

            GameObject obj = Instantiate(
                prefab,
                transform.position + offset,
                finalRotation
            );

            obj.transform.SetParent(transform);
            spawnedCrates.Add(obj.transform);
        }
    }

    private void ReportColors()
    {
        List<CardColor> colors = new List<CardColor>();

        foreach (Transform crateTransform in spawnedCrates)
        {
            CardSpawner cardSpawner = crateTransform.GetComponent<CardSpawner>();

            if (cardSpawner != null)
            {
                // Color STILL comes from cards
                colors.Add(cardSpawner.SelectedPackColor);
            }
        }

        levelCoordinator.ReportColors(colors);
    }

    public void RemoveCrateAndShift(Transform crateToRemove)
    {
        if (!spawnedCrates.Contains(crateToRemove)) return;

        int removedIndex = spawnedCrates.IndexOf(crateToRemove);
        spawnedCrates.RemoveAt(removedIndex);

        Destroy(crateToRemove.gameObject);

        for (int i = removedIndex; i < spawnedCrates.Count; i++)
        {
            Transform crate = spawnedCrates[i];

            Vector3 newPos = transform.position + transform.up * i * spacing;

            Vector3 originalScale = crate.localScale;

            crate.localScale = originalScale * 0.1f;

            crate.DOMove(newPos, 0.3f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    crate.DOScale(originalScale, 0.25f)
                        .SetEase(Ease.OutBack);
                });
        }
    }
}