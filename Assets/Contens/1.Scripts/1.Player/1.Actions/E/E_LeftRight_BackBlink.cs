using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_LeftRight_BackBlink : PlayerActionBlinkBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerPreventStuck playerPreventStuck;
    [SerializeField] private float BLINK_SPEED;
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
        else isCoolTime = true;
    }
    private void CancelBlink()
    {
        isAction = false;

        rb.gravityScale = _gravityScale;
        playerMovement.isLockMoving = false;
        playerPreventStuck.isPreventStuck = true;
    }

    public override void Blink()
    {
        float speed = 0;
        if (base.assignedInput == InputKind.E_Left && playerMovement.isFacingRight) speed = BLINK_SPEED * -1;
        else if (base.assignedInput == InputKind.E_Right && !playerMovement.isFacingRight) speed = BLINK_SPEED;
        else return;

        isAction = true;
        isCoolTime = false;
        _blinkTimer = 0;
        _coolTimer = 0;

        rb.gravityScale = 0;
        playerMovement.isLockMoving = true;
        playerPreventStuck.isPreventStuck = false;

        rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);

        S_SEManager._instance.Play("p_blink");
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
    public override void SwapInAction()
    {
        float speed = 0;
        if (!playerMovement.isFacingRight) speed = BLINK_SPEED;
        else if (playerMovement.isFacingRight) speed = BLINK_SPEED * -1;
        else return;

        rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
    }
}

