using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Up_Warp : PlayerActionNBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WarpPoint;

    public override void InitAction()
    {
        if (onUpWarp && !onUpWarpPast) InitUpWarp();
        else if (onUpWarp && onUpWarpPast) InUpWarp();
        else if (!onUpWarp && onUpWarpPast) EndUpWarp();
        else
        {
            if (base.isCoolDowning) return;

            base.InitAction();
        }
    }
}
