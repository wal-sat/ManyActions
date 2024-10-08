using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Up_UpBlink : PlayerActionTimesLimitBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float BLINK_SPEED;
    [SerializeField] private float BLINK_TIME;

    private float _blinkTimer;
    private bool _isBlinking;
    private float _gravityScale;

    private void Start()
    {
        _gravityScale = rb.gravityScale;
    }
    private void FixedUpdate()
    {
        if (_isBlinking)
        {
            _blinkTimer += Time.deltaTime;

            rb.velocity = new Vector3(0f, BLINK_SPEED * Time.deltaTime, 0f);

            if (_blinkTimer > BLINK_TIME)
            {
                _isBlinking = false;
                rb.velocity = new Vector3(0f, rb.velocity.y / 2, 0f);

                rb.gravityScale = _gravityScale;
                playerMovement.isLockMoving = false;
            }
        }
    }

    public override void InitAction()
    {
        if (_isBlinking || isLimited) return;

        _isBlinking = true;
        _blinkTimer = 0;

        rb.gravityScale = 0;
        playerMovement.isLockMoving = true;

        rb.velocity = new Vector3(0f, 0f, 0f);
    }
}
