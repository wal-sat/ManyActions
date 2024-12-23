using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask ThroughGroundLayer;
    [SerializeField] Transform SwapChecker;
    [SerializeField] Transform LandingChecker;
    [SerializeField] private float SPEED;
    [SerializeField] private float PLUS_SPEED;
    [SerializeField] private float TERMINAL_VELOCITY;

    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool isLockMoving;
    private float _speed;

    private void Start()
    {
        Initialize(true);
    }

    public void Initialize(bool facingRight)
    {
        isFacingRight = facingRight;
        if (isFacingRight) this.transform.localScale = new Vector3(1f, this.transform.localScale.y, 1f);
        else if (!isFacingRight) this.transform.localScale = new Vector3(-1f, this.transform.localScale.y, 1f);

        rb.velocity = Vector3.zero;
    }
    
    //PlayerManagerからFixedUpdateで呼ばれる
    public void MovementUpdate()
    {
        if (Physics2D.OverlapCapsule(SwapChecker.position, new Vector2(0.1f, 0.35f), CapsuleDirection2D.Vertical, 0, GroundLayer) != null)
        {
            if (isKicking && !IsLanding())
            {
                if (isFacingRight) WallKickJump_R();
                else if (!isFacingRight) WallKickJump_L();
            }

            Swap();
        }

        if(!isLockMoving) Move();

        if (rb.velocity.y <= -TERMINAL_VELOCITY * Time.deltaTime) rb.velocity = new Vector3(rb.velocity.x, -TERMINAL_VELOCITY * Time.deltaTime, 0f);

        if (IsLanding() && rb.velocity.y < -0.1f) rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
    }

    private void Move()
    {
        _speed = SPEED;
        if (!isFacingRight) _speed *= -1;
        _speed = Accelerate(_speed);

        rb.velocity = new Vector3(_speed * Time.deltaTime, rb.velocity.y, 0f);
    }
    public void Swap()
    {
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 1f);
        isFacingRight = !isFacingRight;
    }
    public bool IsLanding()
    {
        if (Physics2D.OverlapCircle(LandingChecker.position, 0.1f, GroundLayer) != null) return true;
        if (Physics2D.OverlapCircle(LandingChecker.position, 0.1f, ThroughGroundLayer) != null) return true;
        return false;
    }

    //ーーー加減速ーーー
    [HideInInspector] public bool isRightAccelerate;
    [HideInInspector] public bool isLeftAccelerate;
    private float Accelerate(float speed)
    {
        if (isRightAccelerate) speed += PLUS_SPEED;
        if (isLeftAccelerate) speed -= PLUS_SPEED;

        return speed;
    }

    //ーーーキックーーー
    [HideInInspector] public bool isKicking;
    public Action WallKickJump_L;
    public Action WallKickJump_R;
}
