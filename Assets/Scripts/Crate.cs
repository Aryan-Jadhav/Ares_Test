using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using DG.Tweening;

public class Crate : MonoBehaviour
{

    [SerializeField] private CardColor crateColor;
    [SerializeField] private int maxCapacity = 5;

    private List<Card> storedCards = new List<Card>();

    public void OnTapped()
    {
        StartCoroutine(ReleaseRoutine());
    }

    private IEnumerator ReleaseRoutine()
    {
        Card[] cards = GetComponentsInChildren<Card>();

        foreach (Card card in cards)
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

        storedCards.Add(card);

        Vector3 stackPosition = transform.position + Vector3.up * storedCards.Count * 0.25f;

        card.JumpIntoCrate(stackPosition, () =>
        {
            Debug.Log("card accpeted");
            card.transform.SetParent(transform);
            card.transform.localPosition = Vector3.up * (storedCards.Count - 1) * 0.25f;
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
}