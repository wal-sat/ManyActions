using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Down_Swoop : PlayerActionBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private float SWOOP_SPEED;

    private float _gravityScale;

    private void Awake()
    {
        _gravityScale = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        if (isAction && playerMovement.IsLanding()) EndSwoop();
    }
    private void EndSwoop()
    {
        isAction = false;
        playerMovement.SetLockMovingStatus(this.gameObject, false);
        rb.gravityScale = _gravityScale;
    }

    public override void InitAction()
    {
        isAction = true;
        playerMovement.SetLockMovingStatus(this.gameObject, true);

        rb.gravityScale = 0;
        rb.velocity = new Vector3(0f, -SWOOP_SPEED * Time.deltaTime, 0f);
    }
    public override void EndAction()
    {
        EndSwoop();
    }
    public override void Initialize()
    {
        EndSwoop();
    }
}