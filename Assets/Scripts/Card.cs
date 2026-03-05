using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private CardColor cardColor;
    [SerializeField] private float moveSpeed = 4f;

    private SplineFollower follower;
    private bool isMatched = false;

    public CardColor Color => cardColor;
    public bool IsMatched => isMatched;

    private void Awake()
    {
        follower = GetComponent<SplineFollower>();
        follower.follow = false;
        follower.spline = null;
    }

    public void StartMoving()
    {
        if (isMatched) return;

        transform.SetParent(null);

        Vector3 jumpTarget = GameManager.Instance.ConveyorStartPoint.position;

        AudioManager.Instance.PlayCardToBelt();

        transform.DOJump(jumpTarget, 1f, 1, 0.35f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                follower.spline = GameManager.Instance.MainSpline;
                follower.SetPercent(0);
                follower.followSpeed = moveSpeed;
                follower.follow = true;
            });
    }

    public void StopMoving()
    {
        follower.follow = false;
    }

    public void JumpIntoCrate(Vector3 targetPos, Quaternion targetRotation, System.Action onComplete)
    {
        if (isMatched) return;

        isMatched = true;
        StopMoving();

        float duration = 0.35f;

        transform.DORotateQuaternion(targetRotation, duration)
            .SetEase(Ease.InOutSine);

        transform.DOJump(targetPos, 1.2f, 1, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

}