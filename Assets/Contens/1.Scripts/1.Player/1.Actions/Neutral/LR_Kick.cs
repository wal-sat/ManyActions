using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Kick : PlayerActionBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] float WALLKICKJUMP_POWER;
    [SerializeField] private float JUMP_CANCEL_POWER;
    [SerializeField] float BUFFER_TIME;

    private float _timer;
    private bool _onTimer;
    private bool _wasJumped;

    private void Awake()
    {
        if (base.assignedInput == InputKind.L) playerMovement.WallKickJump_L = WallKickJump;
        if (base.assignedInput == InputKind.R) playerMovement.WallKickJump_R = WallKickJump;
    }
    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > BUFFER_TIME)
            {
                _onTimer = false;
                EndKick();
            }
        }
    }

    private void InitKick()
    {
        _timer = 0;
        _onTimer = true;

        playerMovement.isKicking = true;
    }
    private void EndKick()
    {
        playerMovement.isKicking = false;
    }

    private void WallKickJump()
    {
        _wasJumped = true;
        rb.velocity = new Vector3(rb.velocity.x, WALLKICKJUMP_POWER * Time.deltaTime, 0);

        playerActionJumpManager.OnWallKickJump();
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.L && !playerMovement.isFacingRight) InitKick();
        if (base.assignedInput == InputKind.R && playerMovement.isFacingRight) InitKick();
    }
    public override void EndAction()
    {
        EndKick();

        if (rb.velocity.y > 0 && _wasJumped) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / JUMP_CANCEL_POWER, 0);

        _wasJumped = false;
    }
    public override void Initialize()
    {
        EndKick();
    }
}
