using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpawnCrateData", menuName = "Game/Prefab Spawn Config")]
public class SpawnCrateData : ScriptableObject
{
    public List<GameObject> prefabs;   // Multiple prefabs to spawn
}