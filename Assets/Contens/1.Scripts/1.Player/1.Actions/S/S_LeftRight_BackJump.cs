using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LeftRight_BackJump : PlayerActionJumpBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;
    [SerializeField] private float JUMP_SPEED;

    private bool _isBackJumping;
    private bool _wasFacingRight;
    private float _speed;

    private void FixedUpdate()
    {
        if (_isBackJumping)
        {
            if (_wasFacingRight != playerMovement.isFacingRight) rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, 0);

            _wasFacingRight = playerMovement.isFacingRight;

            if (playerMovement.IsLanding() && rb.velocity.y <= 0) JunpEnd();
        }
    }

    public override void Jump()
    {
        if (base.assignedInput == InputKind.S_Left && playerMovement.isFacingRight) _speed = JUMP_SPEED * -1;
        else if (base.assignedInput == InputKind.S_Right && !playerMovement.isFacingRight) _speed = JUMP_SPEED;
        else return;

        _isBackJumping = true;
        _wasFacingRight = playerMovement.isFacingRight;

        playerMovement.isLockMoving = true;
        rb.velocity = new Vector3(_speed * Time.deltaTime, JUMP_POWER * Time.deltaTime, 0);

        S_SEManager._instance.Play("p_backJump");
    }
    private void JunpEnd()
    {
        _isBackJumping = false;
        playerMovement.isLockMoving = false;
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.S_Left && playerMovement.isFacingRight) base.InitAction();
        else if (base.assignedInput == InputKind.S_Right && !playerMovement.isFacingRight) base.InitAction();
    }
    public override void EndAction()
    {
        JunpEnd();
    }
    public override void Initialize()
    {
        JunpEnd();
    }
}
