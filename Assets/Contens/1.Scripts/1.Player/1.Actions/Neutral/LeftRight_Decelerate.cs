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
        base.InitAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = true;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = true;
    }
    public override void EndAction()
    {
        base.EndAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = false;
    }
    public override void Initialize()
    {
        base.Initialize();
        
        if (base.assignedInput == InputKind.Left) playerMovement.isLeftDecelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightDecelerate = false;
    }
}
