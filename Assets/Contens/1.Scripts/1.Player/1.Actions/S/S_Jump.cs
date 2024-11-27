using UnityEngine;

public class S_Jump : PlayerActionJumpBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;
    [SerializeField] private float JUMP_CANCEL_POWER;


    public override void Jump()
    {
        wasJumped = true;
        rb.velocity = new Vector3(rb.velocity.x, JUMP_POWER * Time.deltaTime, 0);
    }

    public override void EndAction()
    {
        if (rb.velocity.y > 0 && wasJumped) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / JUMP_CANCEL_POWER, 0);

        wasJumped = false;
    }
}
