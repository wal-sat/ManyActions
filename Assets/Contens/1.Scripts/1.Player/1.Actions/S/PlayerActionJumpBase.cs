using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public JumpKind jumpKind;

    public Action<InputKind, JumpKind> init;
    public void Init(Action<InputKind, JumpKind> action)
    {
        init = action;
    }

    public override void InitAction()
    {
        init(assignedInput, jumpKind);
    }

    public virtual void Jump()
    {
        ;
    }
}
