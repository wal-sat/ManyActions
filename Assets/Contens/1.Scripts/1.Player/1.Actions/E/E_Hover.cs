using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Hover : PlayerActionRequireCoolDownBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float HOVER_TIME;
    private float _hoverTimer;
    private bool _isHovering;
    private float _gravityScale;

    private void Start()
    {
        _gravityScale = rb.gravityScale;
    }

    public override void Update()
    {
        base.Update();

        if (_isHovering)
        {
            _hoverTimer += Time.deltaTime;

            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);

            if (_hoverTimer > HOVER_TIME) 
            {
                _isHovering = false;
                rb.gravityScale = _gravityScale;
            }
        }
    }


    public override void InitAction()
    {
        if (base.isCoolDowning) return;

        base.InitAction();

        _hoverTimer = 0;
        _isHovering = true;
        rb.gravityScale = 0;
    }
}