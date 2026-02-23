using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private CardColor _cardColor;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _duration = 5f;

    private Vector3 _targetPosition;
    private bool _isMoving = false;

    public CardColor Color => _cardColor;

    [SerializeField] private float _moveOffset = 1f;

    public void StopMoving()
    {
        _isMoving = false;
    }


    public void MoveOutside()
    {
        transform.SetParent(null);
        transform.DOMoveZ(_moveSpeed, _duration).OnComplete(() =>
        {
            transform.DOMoveX(_moveOffset, _duration);
        }
        );
    }

    private void Update()
    {
        if (!_isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            _isMoving = false;
        }
    }
}