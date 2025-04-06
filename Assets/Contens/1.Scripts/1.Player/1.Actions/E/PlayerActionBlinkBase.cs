using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionBlinkBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;

    [HideInInspector] public bool canNextBlink = true;
    
    public Action<InputKind, ActionKind> init;
    public Func<PlayerActionBlinkBase, bool> isBlinking;

    public override void InitAction()
    {
        init(assignedInput, actionKind);
    }

    public virtual void Blink()
    {
        ;
    }
}
