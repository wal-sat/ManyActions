using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Up_Warp : PlayerActionRequireCoolDownBase
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WarpPoint;

    [HideInInspector] public bool isWarping;

    public override void InitAction()
    {
        if (base.isCoolDowning) return;

        base.InitAction();
        isWarping = true;
    }
}
