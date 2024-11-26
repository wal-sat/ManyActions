using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public JumpKind jumpKind;

    [HideInInspector] public bool wasJumped;
    public Action<InputKind, JumpKind> init;

    public override void InitAction()
    {
        init(assignedInput, jumpKind);
    }

    public virtual void Jump()
    {
        ;
    }
}
