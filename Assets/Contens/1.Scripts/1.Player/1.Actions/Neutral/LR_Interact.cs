using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LR_Interact : PlayerActionBase
{
    [SerializeField] LRBlockManager lrBlockManager;

    public override void InitAction()
    {
        base.InitAction();
        lrBlockManager.LRBlockMove(base.assignedInput);
    }
}
