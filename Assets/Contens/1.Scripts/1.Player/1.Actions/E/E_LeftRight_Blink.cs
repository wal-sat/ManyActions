using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_LeftRight_Blink : PlayerActionTimesLimitBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float BLINK_SPEED;
    [SerializeField] private float BLINK_TIME;

    private float _blinkTimer;
    private bool _isBlinking;
    private float _speed;
    private float _gravityScale;
    private bool _wasFacingRight;

    private void Start()
    {
        _gravityScale = rb.gravityScale;
    }
    private void FixedUpdate()
    {
        if (_isBlinking)
        {
            _blinkTimer += Time.deltaTime;

            if (_wasFacingRight != playerMovement.isFacingRight) _speed *= -1;

            _wasFacingRight = playerMovement.isFacingRight;

            rb.velocity = new Vector3(_speed * Time.deltaTime, 0f, 0f);

            if (_blinkTimer > BLINK_TIME)
            {
                _isBlinking = false;

                rb.gravityScale = _gravityScale;
                playerMovement.isLockMoving = false;
            }
        }
    }

    public override void InitAction()
    {
        if (_isBlinking || isLimited) return;

        if (base.assignedInput == InputKind.E_Left && !playerMovement.isFacingRight) _speed = BLINK_SPEED * -1;
        else if (base.assignedInput == InputKind.E_Right && playerMovement.isFacingRight) _speed = BLINK_SPEED;
        else return;

        _isBlinking = true;
        _blinkTimer = 0;

        rb.gravityScale = 0;
        playerMovement.isLockMoving = true;

        _wasFacingRight = playerMovement.isFacingRight;

        rb.velocity = new Vector3(0f, 0f, 0f);
    }
}
