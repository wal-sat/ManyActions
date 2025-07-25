using System;
using System.Collections.Generic;
using System.Linq;
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
    [HideInInspector] public bool isBlownUpByBarrel;

    public Action OnSwapCallback;

    private Dictionary<GameObject, bool> _isLockMovingDict = new Dictionary<GameObject, bool>();
    private float _speed;
    private bool _isLanding;
    private float _landingTimer;
    private const float LANDING_BUFFER = 0.2f;

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
        _isLanding = IsLanding();
        if (_isLanding)
        {
            if (_landingTimer > LANDING_BUFFER) S_SEManager._instance.Play("p_land");
            _landingTimer = 0;
        }
        else _landingTimer += Time.deltaTime;

        if (Physics2D.OverlapCapsule(SwapChecker.position, new Vector2(0.1f, swapCapsuleSizeY), CapsuleDirection2D.Vertical, 0, GroundLayer) != null)
        {
            if (isKicking && !_isLanding)
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

        if(!_isLockMovingDict.Values.Any(v => v) && !isBlownUpByBarrel) Move();

        if (rb.velocity.y <= -TERMINAL_VELOCITY * Time.deltaTime) rb.velocity = new Vector3(rb.velocity.x, -TERMINAL_VELOCITY * Time.deltaTime, 0f);

        //if (_isLanding && rb.velocity.y < -0.1f) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 10f, 0f);

        if (_isLanding && !isLandingConveyor)
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

        if (OnSwapCallback != null) OnSwapCallback();

        S_SEManager._instance.Play("p_swap");
    }
    public bool IsLanding()
    {
        if (Physics2D.OverlapCapsule(LandingChecker.position, new Vector2(0.2f, 0.01f), CapsuleDirection2D.Horizontal, 0, GroundLayer) != null) return true;
        if (Physics2D.OverlapCapsule(LandingChecker.position, new Vector2(0.2f, 0.01f), CapsuleDirection2D.Horizontal, 0, ThroughGroundLayer) != null) return true;
        return false;
    }
    public void SetLockMovingStatus(GameObject obj, bool isLockMoving)
    {
        _isLockMovingDict[obj] = isLockMoving;
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
