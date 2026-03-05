using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class AcceptingCrateSpawner : MonoBehaviour
{
    [SerializeField] private AcceptingCrateData crateLibrary;
    [SerializeField] private float spacing = 2f;

    private int spawnIndex = 0;
    private List<Transform> acceptingCrates = new List<Transform>();

    public void SpawnSingleAcceptingCrate(CardColor color)
    {
        GameObject prefab = crateLibrary.GetCratePrefab(color);
        if (prefab == null) return;

        Vector3 finalPos = transform.position + transform.forward * spawnIndex * spacing;

        GameObject obj = Instantiate(prefab, finalPos, transform.rotation, transform);

        acceptingCrates.Add(obj.transform);
        spawnIndex++;
    }

    public void RemoveCrateAndShift(Transform crateToRemove)
    {
        if (!acceptingCrates.Contains(crateToRemove)) return;

        int removedIndex = acceptingCrates.IndexOf(crateToRemove);
        acceptingCrates.RemoveAt(removedIndex);

        Destroy(crateToRemove.gameObject);

        for (int i = removedIndex; i < acceptingCrates.Count; i++)
        {
            Transform crate = acceptingCrates[i];

            Vector3 newPos = transform.position + transform.forward * i * spacing;

            crate.DOMove(newPos, 0.3f)
                 .SetEase(Ease.OutCubic);
        }

        spawnIndex = acceptingCrates.Count;
    }
}