using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight_Decelerate : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float MINUS_SPEED;

    private void Start()
    {
        playerMovement._minusSpeed = MINUS_SPEED;
    }

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.Left)
        {
            playerMovement.isLeftDecelerate = true;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightDecelerate = true;
        }
    }
    public override void InAction()
    {
        ;
    }
    public override void EndAction()
    {
        if (base.assignedInput == InputKind.Left)
        {
            playerMovement.isLeftDecelerate = false;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightDecelerate = false;
        }
    }
    public override void Initialize()
    {
        if (base.assignedInput == InputKind.Left)
        {
            playerMovement.isLeftDecelerate = false;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightDecelerate = false;
        }
    }
}
