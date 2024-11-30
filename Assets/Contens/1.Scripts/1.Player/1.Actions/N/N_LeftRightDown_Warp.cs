using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_LeftRightDown_Warp : PlayerActionWarpBase
{
    [SerializeField] GameObject Player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float WARP_DISTANCE;

    private float _distance;

    public override void Warp()
    {
        _distance = WARP_DISTANCE;

        if (assignedInput == InputKind.N_Down)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y - _distance, Player.transform.position.z);
            rb.velocity = new Vector2(0f, 0f);

            return;
        }
        else if (assignedInput == InputKind.N_Left) _distance *= -1;

        Player.transform.position = new Vector3(Player.transform.position.x + _distance, Player.transform.position.y, Player.transform.position.z);
        rb.velocity = new Vector2(0f, 0f);   
    }

    public override void InitAction()
    {
        if (playerActionManager.NBlock)
        {
            if (assignedInput == InputKind.N_Left) playerActionWarpManager.InUpWarp(WarpDirection.left);
            else if (assignedInput == InputKind.N_Right) playerActionWarpManager.InUpWarp(WarpDirection.right);
            else if (assignedInput == InputKind.N_Down) playerActionWarpManager.InUpWarp(WarpDirection.down);
            return;
        }

        base.InitAction();
    }

    public override void InAction()
    {
        if (playerActionWarpManager.isLimited) return;

        if (playerActionManager.NBlock)
        {
            if (assignedInput == InputKind.N_Left) playerActionWarpManager.InUpWarp(WarpDirection.left);
            else if (assignedInput == InputKind.N_Right) playerActionWarpManager.InUpWarp(WarpDirection.right);
            else if (assignedInput == InputKind.N_Down) playerActionWarpManager.InUpWarp(WarpDirection.down);
            return;
        }
    }

    public override void EndAction()
    {
        if (playerActionWarpManager.isLimited) return;

        if (playerActionManager.NBlock)
        {
            if (assignedInput == InputKind.N_Left) playerActionWarpManager.InUpWarp(WarpDirection.left);
            else if (assignedInput == InputKind.N_Right) playerActionWarpManager.InUpWarp(WarpDirection.right);
            else if (assignedInput == InputKind.N_Down) playerActionWarpManager.InUpWarp(WarpDirection.down);
            return;
        }
        else if (!playerActionManager.NBlock && playerActionManager.NBlockPast) 
        {
            playerActionWarpManager.EndUpWarp();
            return;
        }
    }
}
