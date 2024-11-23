using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarpDirection { up, right, left, down, none }

public class N_Up_UpWarp : PlayerActionWarpBase
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WarpPoint;
    [SerializeField] private float WARP_POINT_SPEED;

    private void Start()
    {
        WarpPoint.gameObject.SetActive(false);
    }

    public override void Warp()
    {
        if (playerActionManager.NBlock && !playerActionManager.NBlockPast) playerActionWarpManager.InitUpWarp();
    }

    public override void InitAction()
    {
        if (playerActionManager.NBlock && playerActionManager.NBlockPast) 
        {
            playerActionWarpManager.InUpWarp(WarpDirection.up);
            return;
        }

        base.InitAction();
    }
    public override void InAction()
    {
        if (playerActionWarpManager.isLimited) return;

        if (playerActionManager.NBlock) playerActionWarpManager.InUpWarp(WarpDirection.up);
    }
    public override void EndAction()
    {
        if (playerActionWarpManager.isLimited) return;

        if (playerActionManager.NBlock) playerActionWarpManager.InUpWarp(WarpDirection.up);
        else if (!playerActionManager.NBlock) playerActionWarpManager.EndUpWarp();
    }
    public override void Initialize()
    {
        WarpPoint.gameObject.SetActive(false);
        WarpPoint.transform.position = Player.transform.position;
    }

    //N上のアップワープの処理
    public void InitUpWarp()
    {
        WarpPoint.gameObject.SetActive(true);

        WarpPoint.transform.position = Player.transform.position;
    }
    public void InUpWarp(WarpDirection warpDirection)
    {
        switch (warpDirection)
        {
            case WarpDirection.up:
            case WarpDirection.none:
                WarpPoint.transform.position += new Vector3(0f, WARP_POINT_SPEED * Time.deltaTime, 0f);
            break;
            case WarpDirection.right:
                WarpPoint.transform.position += new Vector3(WARP_POINT_SPEED * Time.deltaTime, 0f, 0f);
            break;
            case WarpDirection.left:
                WarpPoint.transform.position += new Vector3(-WARP_POINT_SPEED * Time.deltaTime, 0f, 0f);
            break;
            case WarpDirection.down:
                WarpPoint.transform.position += new Vector3(0f, -WARP_POINT_SPEED * Time.deltaTime, 0f);
            break;
        }
    }
    public void EndUpWarp()
    {
        if (playerActionWarpManager.isLimited) return;

        Player.transform.position = WarpPoint.transform.position;

        WarpPoint.gameObject.SetActive(false);
    }
}
