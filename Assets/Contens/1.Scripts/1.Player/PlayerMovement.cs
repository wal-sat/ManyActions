using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] LayerMask ThroughGroundLayer;
    [SerializeField] Transform SwapChecker;
    [SerializeField] Transform LandingChecker;
    [SerializeField] Transform OverheadChecker;
    [SerializeField] private float SPEED;
    [SerializeField] private float TERMINAL_VELOCITY;

    [HideInInspector] public bool isFacingRight;
    [HideInInspector] public bool isLockMoving;
    [HideInInspector] public bool isBlownUpByBarrel;

    private float _speed;

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
        if (Physics2D.OverlapCapsule(SwapChecker.position, new Vector2(0.1f, swapCapsuleSizeY), CapsuleDirection2D.Vertical, 0, GroundLayer) != null)
        {
            if (isKicking && !IsLanding())
            {
                if (isFacingRight) WallKickJump_R();
                else if (!isFacingRight) WallKickJump_L();
            }

            if (!isLandingConveyor)
            {
                isLandingConveyor_left = false;
                isLandingConveyor_right = false;
            }

            if (isBlownUpByBarrel) isBlownUpByBarrel = false;

            Swap();
        }

        if (Physics2D.OverlapCircle(OverheadChecker.position, 0.05f, GroundLayer) != null && rb.velocity.y > -0f) rb.velocity = new Vector3(rb.velocity.x, (float) Math.Sqrt(rb.velocity.y), 0f);

        if(!isLockMoving && !isBlownUpByBarrel) Move();

        if (rb.velocity.y <= -TERMINAL_VELOCITY * Time.deltaTime) rb.velocity = new Vector3(rb.velocity.x, -TERMINAL_VELOCITY * Time.deltaTime, 0f);

        if (IsLanding() && rb.velocity.y < -0.1f) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 10f, 0f);

        if (IsLanding() && !isLandingConveyor)
        {
            isLandingConveyor_left = false;
            isLandingConveyor_right = false;
        }
    }

    private void Move()
    {
        _speed = SPEED;
        if (!isFacingRight) _speed *= -1;
        _speed = Accelerate(_speed);
        _speed = Decelerate(_speed);
        _speed = ConveyorAcclerate(_speed);

        rb.velocity = new Vector3(_speed * Time.deltaTime, rb.velocity.y, 0f);
    }
    public void Swap()
    {
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 1f);
        isFacingRight = !isFacingRight;
    }
    public bool IsLanding()
    {
        if (Physics2D.OverlapCircle(LandingChecker.position, 0.01f, GroundLayer) != null) return true;
        if (Physics2D.OverlapCircle(LandingChecker.position, 0.01f, ThroughGroundLayer) != null) return true;
        return false;
    }

    //ーーー加減速ーーー
    [HideInInspector] public float _plusSpeed;
    [HideInInspector] public bool isRightAccelerate;
    [HideInInspector] public bool isLeftAccelerate;
    private float Accelerate(float speed)
    {
        if (isRightAccelerate && isFacingRight) speed += _plusSpeed;
        if (isLeftAccelerate && !isFacingRight) speed -= _plusSpeed;

        return speed;
    }
    [HideInInspector] public float _minusSpeed;
    [HideInInspector] public bool isRightDecelerate;
    [HideInInspector] public bool isLeftDecelerate;
    private float Decelerate(float speed)
    {
        if (isRightDecelerate && !isFacingRight) speed += _minusSpeed;
        if (isLeftDecelerate && isFacingRight) speed -= _minusSpeed;

        return speed;
    }
    

    //ーーーしゃがみーーー
    [HideInInspector] public float swapCapsuleSizeY = 0.35f;

    //ーーーキックーーー
    [HideInInspector] public bool isKicking;
    public Action WallKickJump_L;
    public Action WallKickJump_R;


    //ーーーコンベアーーー
    [HideInInspector] public float _conveyorPlusSpeed;
    [HideInInspector] public bool isLandingConveyor_left;
    [HideInInspector] public bool isLandingConveyor_right;
    [HideInInspector] public bool isLandingConveyor;
    private float ConveyorAcclerate(float speed)
    {
        if (isLandingConveyor_right) speed += _conveyorPlusSpeed;
        if (isLandingConveyor_left) speed -= _conveyorPlusSpeed;

        return speed;
    }
}
