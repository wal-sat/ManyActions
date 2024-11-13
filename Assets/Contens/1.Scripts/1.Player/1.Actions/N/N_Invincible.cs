using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class N_Invincible : PlayerActionNBase
{
    [SerializeField] private float INVINCIBLE_TIME;

    public override void InitAction()
    {
        if (playerActionManager.NBlock) 
        {
            playerActionNManager.InUpWarp(WarpDirection.none);
            return;
        }

        if (base.isCoolDowning) return;

        base.InitAction();

        Debug.Log("Init無敵");
    }

    public override void InAction()
    {
        base.InAction();

        if (playerActionManager.NBlock)
        {
            playerActionNManager.InUpWarp(WarpDirection.none);
            return;
        }

        Debug.Log("In無敵");
    }

    public override void EndAction()
    {
        base.EndAction();

        if (playerActionManager.NBlock)
        {
            playerActionNManager.InUpWarp(WarpDirection.none);
            return;
        }
        else if (!playerActionManager.NBlock && playerActionManager.NBlockPast) 
        {
            playerActionNManager.EndUpWarp();
            return;
        }

        Debug.Log("End無敵");
    }
}
