using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight_Decelerate : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite main;
    [SerializeField] Sprite decelerate;
    [SerializeField] float MINUS_SPEED;

    private void Start()
    {
        playerMovement._minusSpeed = MINUS_SPEED;
    }

    public override void InitAction()
    {
        base.InitAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = true;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = true;
    }
    public override void InAction()
    {
        base.InAction();

        if (base.assignedInput == InputKind.Left && playerMovement.isFacingRight)
        {
            spriteRenderer.sprite = decelerate;
        }
        else if (base.assignedInput == InputKind.Right && !playerMovement.isFacingRight)
        {
            spriteRenderer.sprite = decelerate;
        }
    }
    public override void EndAction()
    {
        base.EndAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = false;

        spriteRenderer.sprite = main;
    }
    public override void Initialize()
    {
        base.Initialize();
        
        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = false;
    }
}
