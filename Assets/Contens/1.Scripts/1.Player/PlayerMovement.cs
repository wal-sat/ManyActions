using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask StageLayer;
    [SerializeField] Transform SwapChecker;
    [SerializeField] Transform LandingChecker;
    [SerializeField] private float SPEED = 200;
    [SerializeField] private float PLUS_SPEED = 200;
    [SerializeField] private float TERMINAL_VELOCITY = 400;

    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool isLockMoving;
    private float _speed;

    private void Start()
    {
        isFacingRight = true;
    }
    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(SwapChecker.position, 0.1f, StageLayer) != null) 
        {
            // if (isHangingWall) HangWall();
            // else Swap();

            Swap();
        }

        if(!isLockMoving) Move();
    }

    private void Move()
    {
        _speed = SPEED;
        if (!isFacingRight) _speed *= -1;
        _speed = Accelerate(_speed);

        rb.velocity = new Vector3(_speed * Time.deltaTime, rb.velocity.y, 0f);
        if (rb.velocity.y <= -TERMINAL_VELOCITY * Time.deltaTime) rb.velocity = new Vector3(_speed * Time.deltaTime, -TERMINAL_VELOCITY * Time.deltaTime, 0f);
    }
    public void Swap()
    {
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 1f);
        isFacingRight = !isFacingRight;
    }
    public bool IsLanding()
    {
        if (Physics2D.OverlapCircle(LandingChecker.position, 0.1f, StageLayer) != null) return true;
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
}