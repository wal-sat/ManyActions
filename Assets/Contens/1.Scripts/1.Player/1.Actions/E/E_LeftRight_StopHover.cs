using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_LeftRight_StopHover : PlayerActionBlinkBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerPreventStuck playerPreventStuck;
    [SerializeField] private float BLINK_TIME;
    [SerializeField] public float ACTION_COOL_TIME;

    private float _blinkTimer;
    private float _coolTimer;
    private float _gravityScale;

    private void Awake()
    {
        _gravityScale = rb.gravityScale;
    }
    private void FixedUpdate()
    {
        if (_blinkTimer < BLINK_TIME) _blinkTimer += Time.deltaTime;
        else if (_blinkTimer >= BLINK_TIME && isAction) CancelBlink();
        if (_coolTimer < ACTION_COOL_TIME) _coolTimer += Time.deltaTime;
        else isCoolTime = false;
    }
    private void CancelBlink()
    {
        isAction = false;

        rb.gravityScale = _gravityScale;
        playerMovement.SetLockMovingStatus(this.gameObject, false);
        playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, false);
    }

    public override void Blink()
    {
        if ((base.assignedInput == InputKind.E_Left && playerMovement.isFacingRight) || (base.assignedInput == InputKind.E_Right && !playerMovement.isFacingRight))
        {
            isAction = true;
            isCoolTime = true;
            _blinkTimer = 0;
            _coolTimer = 0;

            rb.gravityScale = 0;
            playerMovement.SetLockMovingStatus(this.gameObject, true);
            playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, true);

            rb.velocity = new Vector3(0, 0, 0);

            S_SEManager._instance.Play("p_hover");
        }
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.E_Left && playerMovement.isFacingRight) base.InitAction();
        else if (base.assignedInput == InputKind.E_Right && !playerMovement.isFacingRight) base.InitAction();
    }
    public override void EndAction()
    {
        ;
    }
    public override void Initialize()
    {
        CancelBlink();
    }
}
