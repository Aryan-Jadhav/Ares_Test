using UnityEngine;
using System.Collections.Generic;

public class Crate : MonoBehaviour
{
    [SerializeField] private CardColor crateColor;
    [SerializeField] private int maxCapacity = 5;

    private List<Card> storedCards = new List<Card>();

    public void OnTapped()
    {
        Card[] cards = GetComponentsInChildren<Card>();

        foreach (Card card in cards)
        {
            card.MoveOutside();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        // Layer Check
        if (other.gameObject.layer != LayerMask.NameToLayer("Card"))
        {
            Debug.Log("Rejected: Not on Card layer");
            return;
        }

        Debug.Log("Layer is Card");

        // Get Card component
        Card card = other.GetComponent<Card>();
        if (card == null)
        {
            Debug.Log("Rejected: No Card component found");
            return;
        }

        Debug.Log("Card component found");

        // Color check
        if (card.Color != crateColor)
        {
            Debug.Log("Rejected: Color mismatch. Card: "
                      + card.Color + " | Crate: " + crateColor);
            return;
        }

        Debug.Log("Color matched! Accepting card.");

        AcceptCard(card);
    }

    private void AcceptCard(Card card)
    {
        storedCards.Add(card);

        card.StopMoving();

        card.transform.SetParent(transform);
        card.transform.localPosition = Vector3.up * storedCards.Count * 0.25f;

        if (storedCards.Count >= maxCapacity)
        {
            Debug.Log("Crate Full!");
            gameObject.SetActive(false);
        }
    }
}