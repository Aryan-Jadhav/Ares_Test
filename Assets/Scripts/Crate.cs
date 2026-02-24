using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using DG.Tweening;

public class Crate : MonoBehaviour
{

    [SerializeField] private CardColor crateColor;
    [SerializeField] private int maxCapacity = 5;

    [SerializeField] private Transform[] acceptPoints;
    public CardColor CrateColor => crateColor;

    private List<Card> storedCards = new List<Card>();

    private List<Card> myCards = new List<Card>();

    public void OnTapped()
    {
        StartCoroutine(ReleaseRoutine());
    }

    private IEnumerator ReleaseRoutine()
    {
        foreach (Card card in myCards)
        {
            if (!card.IsMatched)
            {
                card.StartMoving();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Card")) return;

        Debug.Log("card entered");
        Card card = other.GetComponent<Card>();
        if (card == null) return;
        if (card.Color != crateColor) return;
        if (card.IsMatched) return;

        AcceptCard(card);
    }

    private void AcceptCard(Card card)
    {
        if (storedCards.Count >= acceptPoints.Length) return;

        Transform targetPoint = acceptPoints[storedCards.Count];

        storedCards.Add(card);

        card.JumpIntoCrate(targetPoint.position, () =>
        {
            card.transform.SetParent(transform, true);
        });

        if (storedCards.Count >= maxCapacity)
        {
            AnimateCrateDisappear();
        }
    }

    private void AnimateCrateDisappear()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);

        transform.DOScale(Vector3.zero, 0.3f)
            .SetDelay(0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }

    public void RegisterCard(Card card)
    {
        myCards.Add(card);
    }
}