using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Grab : PlayerActionBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerPreventStuck playerPreventStuck;
    [SerializeField] RopeManager ropeManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite main;
    [SerializeField] Sprite grab;

    [HideInInspector] public bool isOverlapRope;

    private float _gravityScale;

    private void Awake()
    {
        _gravityScale = rb.gravityScale;
    }

    public override void InitAction()
    {
        if (ropeManager.IsOverlapRope())
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector3(0f, 0f, 0f);
            playerMovement.isLockMoving = true;
            playerPreventStuck.isPreventStuck = false;
        }
        spriteRenderer.sprite = grab;
    }
    public override void InAction()
    {
        if (ropeManager.IsOverlapRope())
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector3(0f, 0f, 0f);
            playerMovement.isLockMoving = true;
            playerPreventStuck.isPreventStuck = false;
        }
    }
    public override void EndAction()
    {
        if (ropeManager.IsOverlapRope())
        {
            rb.gravityScale = _gravityScale;
            playerMovement.isLockMoving = false;
            playerPreventStuck.isPreventStuck = true;
        }
        spriteRenderer.sprite = main;
    }
    public override void Initialize()
    {
        rb.gravityScale = _gravityScale;
        playerMovement.isLockMoving = false;
        playerPreventStuck.isPreventStuck = true;
        
        spriteRenderer.sprite = main;
    }
}
