using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Swap : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;

    [HideInInspector] public bool isSwaping;

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.L1 && playerMovement.isFacingRight) playerMovement.Swap();
        else if (base.assignedInput == InputKind.R1 && !playerMovement.isFacingRight) playerMovement.Swap();
        else return;

        base.InitAction();

        isSwaping = true;
    }
    public override void EndAction()
    {
        base.EndAction();
        
        isSwaping = false;
    }
}
