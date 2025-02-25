using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes.Test;
using UnityEngine;

public enum Direction { up, down, left, right, up_left, up_right, down_left, down_right }

public class BallMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform leftChecker;
    [SerializeField] Transform rightChecker;
    [SerializeField] Transform topChecker;
    [SerializeField] Transform downChecker;
    [SerializeField] LayerMask WallLayer;

    [SerializeField] float COOL_TIME;
    [SerializeField] float SPEED;

    private bool _isCoolTime;
    private float _timer;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (!_isCoolTime)
        {
            if (Physics2D.OverlapCircle(leftChecker.position, 0.1f, WallLayer) != null) Bound(Direction.left);
            if (Physics2D.OverlapCircle(rightChecker.position, 0.1f, WallLayer) != null) Bound(Direction.right);
            if (Physics2D.OverlapCircle(topChecker.position, 0.1f, WallLayer) != null) Bound(Direction.up);
            if (Physics2D.OverlapCircle(downChecker.position, 0.1f, WallLayer) != null) Bound(Direction.down                                                );
        }
        else 
        {
            _timer += Time.deltaTime;
            if (_timer > COOL_TIME)
            {
                _isCoolTime = false;
            }
        }
    }

    public void Init()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;

        float speed = UnityEngine.Random.Range(SPEED - 2.0f, SPEED + 2.0f);

        rb.velocity = randomDirection * speed;
    }

    private void Bound(Direction direction)
    {
        if (direction == Direction.up) rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        if (direction == Direction.down) rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        if (direction == Direction.left) rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        if (direction == Direction.right) rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);

        _isCoolTime = true;
        _timer = 0;
    }

    /*
    private Vector2 ConvertDirection(int directionIndex)
    {
        Direction direction = (Direction) Enum.ToObject(typeof(Direction), directionIndex);

        if (direction == Direction.up) return Vector2.up;
        if (direction == Direction.up_right) return ( Vector2.up + Vector2.right ).normalized;
        if (direction == Direction.right) return Vector2.right;
        if (direction == Direction.down_right) return ( Vector2.down + Vector2.right ).normalized;
        if (direction == Direction.down) return Vector2.down;
        if (direction == Direction.down_left) return ( Vector2.down + Vector2.left ).normalized;
        if (direction == Direction.left) return Vector2.left;
        if (direction == Direction.up_left) return ( Vector2.up + Vector2.left ).normalized;

        return Vector2.zero;
    }
    */
}
