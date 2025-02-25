using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Swap : PlayerActionBase
{
    [SerializeField] PlayerMovement playerMovement;

    [HideInInspector] public bool isSwaping;

    public override void InitAction()
    {
        if (base.assignedInput == InputKind.L && playerMovement.isFacingRight) playerMovement.Swap();
        else if (base.assignedInput == InputKind.R && !playerMovement.isFacingRight) playerMovement.Swap();
        else return;

        isSwaping = true;
    }
    public override void EndAction()
    {
        isSwaping = false;
    }
}
