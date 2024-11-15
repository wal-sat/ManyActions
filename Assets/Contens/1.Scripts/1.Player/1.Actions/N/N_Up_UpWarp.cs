using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarpDirection { up, right, left, down, none }

public class N_Up_UpWarp : PlayerActionNBase
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WarpPoint;
    [SerializeField] private float WARP_POINT_SPEED;

    private bool _coolTimeBlock;

    private void Start()
    {
        WarpPoint.gameObject.SetActive(false);
    }

    public override void InitAction()
    {
        if (base.isCoolDowning || _coolTimeBlock)
        {
            _coolTimeBlock = true;
            return;
        }

        base.InitAction();

        if (playerActionManager.NBlock && !playerActionManager.NBlockPast) playerActionNManager.InitUpWarp();
        else if (playerActionManager.NBlock && playerActionManager.NBlockPast) playerActionNManager.InUpWarp(WarpDirection.up);
    }
    public override void InAction()
    {
        if (playerActionManager.NBlock) playerActionNManager.InUpWarp(WarpDirection.up);
    }
    public override void EndAction()
    {
        base.EndAction();

        if (playerActionManager.NBlock) playerActionNManager.InUpWarp(WarpDirection.up);
        else if (!playerActionManager.NBlock) playerActionNManager.EndUpWarp();
    }

    //N上のアップワープの処理
    public void InitUpWarp()
    {
        if (_coolTimeBlock) return;

        Debug.Log("Init Up Warp");
        WarpPoint.gameObject.SetActive(true);

        WarpPoint.transform.position = Player.transform.position;
    }
    public void InUpWarp(WarpDirection warpDirection)
    {
        if (_coolTimeBlock) return;

        Debug.Log("In Up Warp");
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
        if (_coolTimeBlock) 
        {
            _coolTimeBlock = false;
            return;
        }
        Debug.Log("End Up Warp");

        Player.transform.position = WarpPoint.transform.position;

        WarpPoint.gameObject.SetActive(false);
    }
}
