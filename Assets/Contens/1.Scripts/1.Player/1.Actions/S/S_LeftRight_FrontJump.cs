using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LeftRight_FrontJump : PlayerActionJumpBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;
    [SerializeField] private float JUMP_SPEED;
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

        playerMovement.SetLockMovingStatus(this.gameObject, false);

        isAction = false;
        wasJumped = false;
    }

    public override void Jump()
    {
        float speed = 0;

        if (base.assignedInput == InputKind.S_Left && !playerMovement.isFacingRight) speed = JUMP_SPEED * -1;
        else if (base.assignedInput == InputKind.S_Right && playerMovement.isFacingRight) speed = JUMP_SPEED;
        else return;

        playerMovement.SetLockMovingStatus(this.gameObject, true);

        isAction = true;
        wasJumped = true;
        isCoolTime = true;

        _cancelTimer = 0;
        _coolTimer = 0;
        _canCancel = false;
        _inputCancel = false;
        
        rb.velocity = new Vector3(speed * Time.deltaTime, JUMP_POWER * Time.deltaTime, 0);

        S_SEManager._instance.Play("p_frontJump");
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.S_Left && !playerMovement.isFacingRight) base.InitAction();
        else if (base.assignedInput == InputKind.S_Right && playerMovement.isFacingRight) base.InitAction();
    }
    public override void EndAction()
    {
        _inputCancel = true;
    }
    public override void Initialize()
    {
        JumpCancel();
    }
    public override void SwapInAction()
    {
        float speed = 0;

        if (!playerMovement.isFacingRight) speed = JUMP_SPEED * -1;
        else if (playerMovement.isFacingRight) speed = JUMP_SPEED;
        else return;

        rb.velocity = new Vector3(speed * Time.deltaTime, rb.velocity.y, 0);
    }
}
