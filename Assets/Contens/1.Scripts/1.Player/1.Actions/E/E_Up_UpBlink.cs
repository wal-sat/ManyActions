using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Up_UpBlink : PlayerActionBlinkBase
{
    [SerializeField] Rigidbody2D rb;
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
        else isCoolTime = false;
    }
    
    private void CancelBlink()
    {
        rb.velocity = new Vector3(0f, rb.velocity.y / 2, 0f);

        isAction = false;

        rb.gravityScale = _gravityScale;
        playerMovement.SetLockMovingStatus(this.gameObject, false);
    }

    public override void Blink()
    {
        isAction = true;
        isCoolTime = true;
        _blinkTimer = 0;
        _coolTimer = 0;

        rb.gravityScale = 0;
        playerMovement.SetLockMovingStatus(this.gameObject, true);

        rb.velocity = new Vector3(0f, BLINK_SPEED * Time.deltaTime, 0f);

        S_SEManager._instance.Play("p_upBlink");
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
