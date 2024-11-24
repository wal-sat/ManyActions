using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class N_Invincible : PlayerActionRequireCoolDownBase
{
    [SerializeField] public PlayerActionManager playerActionManager;
    [SerializeField] public PlayerActionWarpManager playerActionWarpManager;
    [SerializeField] private float INVINCIBLE_TIME;

    private void EndInvincible()
    {

    }

    public override void InitAction()
    {
        if (playerActionManager.NBlock) 
        {
            playerActionWarpManager.InUpWarp(WarpDirection.none);
            return;
        }

        if (base.isCoolDowning) return;

        base.InitAction();

        Debug.Log("Init無敵");
    }
    public override void InAction()
    {
        if (!playerActionWarpManager.isLimited)
        {
            if (playerActionManager.NBlock)
            {
                playerActionWarpManager.InUpWarp(WarpDirection.none);
                return;
            }
        }

        Debug.Log("In無敵");
    }
    public override void EndAction()
    {
        if (!playerActionWarpManager.isLimited)
        {
            if (playerActionManager.NBlock)
            {
                playerActionWarpManager.InUpWarp(WarpDirection.none);
                return;
            }
            else if (!playerActionManager.NBlock && playerActionManager.NBlockPast) 
            {
                playerActionWarpManager.EndUpWarp();
                return;
            }
        }

        Debug.Log("End無敵");
        EndInvincible();
    }
    public override void Initialize()
    {
        EndInvincible();
    }
}
