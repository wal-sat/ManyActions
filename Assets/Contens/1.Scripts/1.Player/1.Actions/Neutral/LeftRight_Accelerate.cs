using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight_Accelerate : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite main;
    [SerializeField] Sprite accelerate;
    [SerializeField] float PLUS_SPEED;

    private void Start()
    {
        playerMovement._plusSpeed = PLUS_SPEED;
    }

    public override void InitAction()
    {
        base.InitAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftAccelerate = true;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightAccelerate = true;
    }
    public override void InAction()
    {
        base.InAction();

        if (base.assignedInput == InputKind.Left && !playerMovement.isFacingRight)
        {
            spriteRenderer.sprite = accelerate;
        }
        else if (base.assignedInput == InputKind.Right && playerMovement.isFacingRight)
        {
            spriteRenderer.sprite = accelerate;
        }
    }
    public override void EndAction()
    {
        base.EndAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftAccelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightAccelerate = false;

        spriteRenderer.sprite = main;
    }
    public override void Initialize()
    {
        base.Initialize();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftAccelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightAccelerate = false;
        
        spriteRenderer.sprite = main;
    }
}
