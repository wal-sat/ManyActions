using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Up_BigJump : PlayerActionJumpBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;
    [SerializeField] private float JUMP_CANCEL_POWER;
    [SerializeField] private float CANCEL_TIME;
    [SerializeField] public float ACTION_COOL_TIME;


    private float _cancelTimer;
    private float _coolTimer;
    private bool _canCancel;
    private bool _inputCancel;

    private void FixedUpdate()
    {
        if (_cancelTimer < CANCEL_TIME) _cancelTimer += Time.deltaTime;
        else _canCancel = true;    
        if (_coolTimer < ACTION_COOL_TIME) _coolTimer += Time.deltaTime;
        else isCoolTime = false;
        
        if (isAction)
        {
            if (playerMovement.IsLanding() && rb.velocity.y < 0) JumpCancel();
            if (_canCancel && _inputCancel) JumpCancel();
        }
    }

    private void JumpCancel()
    {
        if (rb.velocity.y > 0 && wasJumped && !isJumping(this)) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / JUMP_CANCEL_POWER, 0);

        isAction = false;
        wasJumped = false;
    }

    public override void Jump()
    {
        isAction = true;
        wasJumped = true;
        isCoolTime = true;

        _cancelTimer = 0;
        _coolTimer = 0;
        _canCancel = false;
        _inputCancel = false;

        rb.velocity = new Vector3(rb.velocity.x, JUMP_POWER * Time.deltaTime, 0);

        S_SEManager._instance.Play("p_bigJump");
    }
    public override void EndAction()
    {
        _inputCancel = true;
    }
    public override void Initialize()
    {
        JumpCancel();
    }
}
