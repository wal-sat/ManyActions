using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Swap : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;
    public override void InitAction()
    {
        if (base.assignedInput == InputKind.L && playerMovement.isFacingRight) playerMovement.Swap();
        if (base.assignedInput == InputKind.R && !playerMovement.isFacingRight) playerMovement.Swap();
    }
    public override void InAction()
    {
        ;
    }
    public override void EndAction()
    {
        ;
    }
}
