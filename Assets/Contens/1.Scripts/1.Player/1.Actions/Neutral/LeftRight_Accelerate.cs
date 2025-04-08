using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRight_Accelerate : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;
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
    public override void EndAction()
    {
        base.EndAction();

        if (base.assignedInput == InputKind.Left) playerMovement.isLeftAccelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightAccelerate = false;
    }
    public override void Initialize()
    {
        base.Initialize();
        
        if (base.assignedInput == InputKind.Left) playerMovement.isLeftAccelerate = false;
        if (base.assignedInput == InputKind.Right) playerMovement.isRightAccelerate = false;
    }
}
