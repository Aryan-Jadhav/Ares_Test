using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "theSpawnCrateData", menuName = "Game/Prefab Spawn Config")]
public class SpawnCrateData : ScriptableObject
{
    public List<GameObject> prefabs;   // Multiple prefabs to spawn
}
