using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Up_BigJump : PlayerActionJumpBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;
    [SerializeField] private float JUMP_CANCEL_POWER;
    [SerializeField] private float CANCEL_TIME;


    private float _timer;
    private bool _onTimer;
    private bool _canCancel;
    private bool _inputCancel;

    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;
            if (_timer > CANCEL_TIME)
            {
                _canCancel = true;
            }

            if (_canCancel && _inputCancel) JumpCancel();
        }
    }

    private void JumpCancel()
    {
        _onTimer = false;
        if (rb.velocity.y > 0 && wasJumped) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / JUMP_CANCEL_POWER, 0);

        wasJumped = false;
    }

    public override void Jump()
    {
        wasJumped = true;

        _timer = 0;
        _onTimer = true;
        _canCancel = false;
        _inputCancel = false;

        rb.velocity = new Vector3(rb.velocity.x, JUMP_POWER * Time.deltaTime, 0);
    }
    public override void EndAction()
    {
        _inputCancel = true;
    }
}
