using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight_Accelerate : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.Left)
        {
            playerMovement.isLeftAccelerate = true;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightAccelerate = true;
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
            playerMovement.isLeftAccelerate = false;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightAccelerate = false;
        }
    }
    public override void Initialize()
    {
        if (base.assignedInput == InputKind.Left)
        {
            playerMovement.isLeftAccelerate = false;
        }
        if (base.assignedInput == InputKind.Right)
        {
            playerMovement.isRightAccelerate = false;
        }
    }
}
