using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Kick : PlayerActionBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] GameObject kick;
    [SerializeField] LR_Swap lr_Swap;
    [SerializeField] float WALLKICKJUMP_POWER;
    [SerializeField] private float JUMP_CANCEL_POWER;
    [SerializeField] private float CANCEL_TIME;
    [SerializeField] public float ACTION_COOL_TIME;

    private PlayerKickAnimation playerKickAnimation;
    private Collider2D kickBall;

    private float _cancelTimer;
    private float _coolTimer;
    private bool _canCancel;
    private bool _inputCancel;
    private bool _wasJumped;

    private void Awake()
    {
        if (base.assignedInput == InputKind.L) playerMovement.WallKickJump_L = WallKickJump;
        if (base.assignedInput == InputKind.R) playerMovement.WallKickJump_R = WallKickJump;

        playerKickAnimation = kick.GetComponent<PlayerKickAnimation>();
        kickBall = kick.GetComponent<Collider2D>();

        kickBall.enabled = false;
    }
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

    private void WallKickJump()
    {
        _wasJumped = true;
        rb.velocity = new Vector3(rb.velocity.x, WALLKICKJUMP_POWER * Time.deltaTime, 0);

        playerActionJumpManager.OnWallKickJump();
    }
    private void JumpCancel()
    {
        if (rb.velocity.y > 0 && _wasJumped) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / JUMP_CANCEL_POWER, 0);

        kickBall.enabled = false;
        playerMovement.isKicking = false;

        isAction = false;
        _wasJumped = false;
    }

    public override void InitAction()
    {
        if (lr_Swap.isSwaping) return;

        if ( ( base.assignedInput == InputKind.L && !playerMovement.isFacingRight ) || ( base.assignedInput == InputKind.R && playerMovement.isFacingRight ) )
        {
            isAction = true;
            _wasJumped = true;
            isCoolTime = true;

            _cancelTimer = 0;
            _coolTimer = 0;
            _canCancel = false;
            _inputCancel = false;

            kickBall.enabled = true;
            playerMovement.isKicking = true;

            playerKickAnimation.AnimationStart();

            S_SEManager._instance.Play("p_kick");
        }

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
