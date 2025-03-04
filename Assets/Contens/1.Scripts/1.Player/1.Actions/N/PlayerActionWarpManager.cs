using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionWarpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionWarpBase[] warpActions;
    
    [HideInInspector] public int maxWarpTimes;
    [HideInInspector] public bool isLimited;

    private int _warpTimes;
    private int _maxWarpTimes;

    private void Start()
    {
        foreach (var action in warpActions)
        {
            if (action == null) continue;

            action.init = Init;
        }
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (_warpTimes == 0) 
        {
            isLimited = true;
            return;
        }
        if (_warpTimes != -1) _warpTimes --;
        isLimited = false;

        foreach (var action in warpActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Warp();
        }
    }

    public void Recure()
    {
        _maxWarpTimes = SetMaxWarpTimes();
        _warpTimes = _maxWarpTimes;
    }
    private int SetMaxWarpTimes()
    {
        int maxWarpTimes = 0;
        foreach (var action in warpActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.N_UpWarp:
                case ActionKind.N_Warp:
                    if (action.isEnable && maxWarpTimes == 0) maxWarpTimes = 1;
                    break;
                case ActionKind.N_DoubleWarp:
                    if (action.isEnable && ( maxWarpTimes == 0 || maxWarpTimes == 1 ) ) maxWarpTimes = 2;
                    break;
                case ActionKind.N_InfiniteWarp:
                    if (action.isEnable) maxWarpTimes = -1;
                    break;
            }
        }
        return maxWarpTimes;
    }

    public void ChangeWarpTimes()
    {
        int maxWarpTimes = 0;
        foreach (var action in warpActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.N_UpWarp:
                case ActionKind.N_Warp:
                    if (action.isEnable && maxWarpTimes == 0) maxWarpTimes = 1;
                    break;
                case ActionKind.N_DoubleWarp:
                    if (action.isEnable && ( maxWarpTimes == 0 || maxWarpTimes == 1 ) ) maxWarpTimes = 2;
                    break;
                case ActionKind.N_InfiniteWarp:
                    if (action.isEnable) maxWarpTimes = -1;
                    break;
            }
        }
        
        if (maxWarpTimes == -1) _warpTimes = -1;
        else _warpTimes -= _maxWarpTimes - maxWarpTimes;
    }

    //ーーーUpWarpの処理ーーー
    [SerializeField] N_Up_UpWarp n_Up_UpWarp;

    public void InitUpWarp()
    {
        n_Up_UpWarp.InitUpWarp();
    }
    public void InUpWarp(WarpDirection warpDirection)
    {
        n_Up_UpWarp.InUpWarp(warpDirection);
    }
    public void EndUpWarp()
    {
        n_Up_UpWarp.EndUpWarp();
    }
}
