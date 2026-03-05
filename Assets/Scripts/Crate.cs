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

        // Wait one frame to ensure re-parenting happened
        yield return null;

        CheckIfEmpty();
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

        AudioManager.Instance.PlayBeltToAccept();

        card.JumpIntoCrate(
            targetPoint.position,
            targetPoint.rotation,
            () =>
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

        AudioManager.Instance.PlayCrateDestroy();

        transform.DOScale(Vector3.zero, 0.3f)
            .SetDelay(0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                AcceptingCrateSpawner spawner = GetComponentInParent<AcceptingCrateSpawner>();

                if (spawner != null)
                {
                    spawner.RemoveCrateAndShift(transform);
                }
                else
                {
                    Destroy(gameObject);
                }
            });
    }

    public void RegisterCard(Card card)
    {
        myCards.Add(card);
    }

    private void CheckIfEmpty()
    {
        bool hasAnyChildCards = false;

        foreach (Card card in myCards)
        {
            if (card != null && card.transform.parent == transform)
            {
                hasAnyChildCards = true;
                break;
            }
        }

        if (!hasAnyChildCards)
        {
            DestroyAndShift();
        }
    }

    private void DestroyAndShift()
    {
        SpawnCrateSpawner spawner = GetComponentInParent<SpawnCrateSpawner>();

        if (spawner != null)
        {
            spawner.RemoveCrateAndShift(transform);
        }

    }

    public void SetCrateColor(CardColor color)
    {
        crateColor = color;
    }
}