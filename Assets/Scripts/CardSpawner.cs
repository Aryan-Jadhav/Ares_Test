using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private CardLibrary cardLibrary;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int cardCount = 6;

    private CardColor selectedPackColor;
    public CardColor SelectedPackColor => selectedPackColor;

    private Crate crate;

    private void Awake()
    {
        crate = GetComponent<Crate>();
    }

    private void Start()
    {
        SpawnCards();
    }

    private void SpawnCards()
    {
        Card selectedPrefab = GetRandomCardPrefabExcludingCrateColor();

        selectedPackColor = selectedPrefab.Color;

        for (int i = 0; i < cardCount && i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            Card card = Instantiate(
                selectedPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            card.transform.SetParent(transform, true);
            crate.RegisterCard(card);
        }
    }

    private Card GetRandomCardPrefabExcludingCrateColor()
    {
        List<Card> validPrefabs = new List<Card>();

        foreach (Card card in cardLibrary.cardPrefabs)
        {
            if (card.Color != crate.CrateColor)
            {
                validPrefabs.Add(card);
            }
        }

        int randomIndex = Random.Range(0, validPrefabs.Count);
        return validPrefabs[randomIndex];
    }
}