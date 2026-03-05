using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CrateEntry
{
    public CardColor color;
    public GameObject cratePrefab;
}

[CreateAssetMenu(fileName = "AcceptingCrateData", menuName = "Game/Accepting Crate Library")]
public class AcceptingCrateData : ScriptableObject
{
    public List<CrateEntry> crateEntries;

    public GameObject GetCratePrefab(CardColor color)
    {
        foreach (var entry in crateEntries)
        {
            if (entry.color == color)
                return entry.cratePrefab;
        }

        Debug.LogError("No crate prefab found for color: " + color);
        return null;
    }
}