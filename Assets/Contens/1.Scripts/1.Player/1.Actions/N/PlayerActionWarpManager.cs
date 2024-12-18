using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarpKind { Warp, UpWarp }

public class PlayerActionWarpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionWarpBase[] warpActions;
    [SerializeField] int MAX_WARP_TIMES;

    [HideInInspector] public bool isLimited;

    private int _warpTimes;

    private void Start()
    {
        foreach (var action in warpActions)
        {
            if (action == null) continue;

            action.init = Init;
        }
    }

    private void Update()
    {
        if (playerMovement.IsLanding()) _warpTimes = MAX_WARP_TIMES;
    }

    private void Init(InputKind inputKind, WarpKind warpKind)
    {
        if (_warpTimes <= 0) 
        {
            isLimited = true;
            return;
        }
        _warpTimes --;
        isLimited = false;

        foreach (var action in warpActions)
        {
            if (action == null) continue;

            if (action.warpKind == warpKind && action.assignedInput == inputKind) action.Warp();
        }
    }


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
