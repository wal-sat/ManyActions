using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;

    [HideInInspector] public bool wasJumped;
    public Action<InputKind, ActionKind> init;

    public override void InitAction()
    {
        init(assignedInput, actionKind);
    }

    public virtual void Jump()
    {
        ;
    }
}
