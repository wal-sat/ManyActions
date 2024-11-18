using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionBlinkBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public BlinkKind blinkKind;
    public Action<InputKind, BlinkKind> init;

    public override void InitAction()
    {
        init(assignedInput, blinkKind);
    }

    public virtual void Blink()
    {
        ;
    }
}
