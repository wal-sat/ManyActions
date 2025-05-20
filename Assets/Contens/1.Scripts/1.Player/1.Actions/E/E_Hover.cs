using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Hover : PlayerActionBlinkBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float HOVER_TIME;
    [SerializeField] public float ACTION_COOL_TIME;
    
    private float _hoverTimer;
    private float _coolTimer;
    private float _gravityScale;

    private void Awake()
    {
        _gravityScale = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        if (_hoverTimer < HOVER_TIME) _hoverTimer += Time.deltaTime;
        else if (_hoverTimer >= HOVER_TIME && isAction) CancelHover();
        if (_coolTimer < ACTION_COOL_TIME) _coolTimer += Time.deltaTime;
        else isCoolTime = false;
    }
    private void CancelHover()
    {
        isAction = false;
        rb.gravityScale = _gravityScale;
    }

    public override void Blink()
    {
        isAction = true;
        isCoolTime = true;
        _hoverTimer = 0;
        _coolTimer = 0;

        rb.gravityScale = 0;
        rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);

        S_SEManager._instance.Play("p_hover");
    }
    public override void EndAction()
    {
        ;
    }
    public override void Initialize()
    {
        CancelHover();
    }
}
