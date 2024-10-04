using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Invincible : PlayerActionRequireCoolDownBase
{
    [SerializeField] N_Up_Warp n_Up_Warp;
    [SerializeField] private float INVINCIBLE_TIME;

    public override void InitAction()
    {
        if (n_Up_Warp.isWarping)
        {
            return;
        }
        if (base.isCoolDowning) return;

        base.InitAction();
    }
}
