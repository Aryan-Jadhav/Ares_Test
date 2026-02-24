using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "Game/Card Library")]
public class CardLibrary : ScriptableObject
{
    public List<Card> cardPrefabs;
}