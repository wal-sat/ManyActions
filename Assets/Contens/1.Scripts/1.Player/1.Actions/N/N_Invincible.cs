using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class N_Invincible : PlayerActionNBase
{
    [SerializeField] private float INVINCIBLE_TIME;

    public override void InitAction()
    {
        if (onUpWarp && !onUpWarpPast) InitUpWarp();
        else if (onUpWarp && onUpWarpPast) InUpWarp();
        else if (!onUpWarp && onUpWarpPast) EndUpWarp();
        else
        {
            
        }
        
        if (base.isCoolDowning) return;

        base.InitAction();
    }
}
