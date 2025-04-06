using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_LeftRight_Blink : PlayerActionBlinkBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float BLINK_SPEED;
    [SerializeField] private float BLINK_TIME;
    [SerializeField] public float NEXT_BLINK_BUFFER_TIME;

    private float _blinkTimer;
    private float _nextBlinkBufferTimer;
    private float _gravityScale;

    private void Awake()
    {
        _gravityScale = rb.gravityScale;
    }
    private void FixedUpdate()
    {
        if (_blinkTimer < BLINK_TIME) _blinkTimer += Time.deltaTime;
        else if (_blinkTimer >= BLINK_TIME && isAction) CancelBlink();
        if (_nextBlinkBufferTimer < NEXT_BLINK_BUFFER_TIME) _nextBlinkBufferTimer += Time.deltaTime;
        else canNextBlink = true;
    }
    private void CancelBlink()
    {
        isAction = false;

        rb.gravityScale = _gravityScale;
        playerMovement.isLockMoving = false;
    }

    public override void Blink()
    {
        float speed = 0;

        if (base.assignedInput == InputKind.E_Left && !playerMovement.isFacingRight) speed = BLINK_SPEED * -1;
        else if (base.assignedInput == InputKind.E_Right && playerMovement.isFacingRight) speed = BLINK_SPEED;
        else return;

        isAction = true;
        canNextBlink = false;
        _blinkTimer = 0;
        _nextBlinkBufferTimer = 0;

        rb.gravityScale = 0;
        playerMovement.isLockMoving = true;

        rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);

        S_SEManager._instance.Play("p_blink");
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.E_Left && !playerMovement.isFacingRight) base.InitAction();
        else if (base.assignedInput == InputKind.E_Right && playerMovement.isFacingRight) base.InitAction();
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

        if (!playerMovement.isFacingRight) speed = BLINK_SPEED * -1;
        else if (playerMovement.isFacingRight) speed = BLINK_SPEED;
        else return;

        rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
