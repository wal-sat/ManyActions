using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;

    [HideInInspector] public bool canNextJump = true;
    [HideInInspector] public bool wasJumped;

    public Action<InputKind, ActionKind> init;
    public Func<PlayerActionJumpBase, bool> isJumping;

    public override void InitAction()
    {
        base.InitAction();
        init(assignedInput, actionKind);
    }

    public virtual void Jump()
    {
        ;
    }
}
