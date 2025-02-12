using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionWarpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] PlayerActionWarpBase[] warpActions;
    
    [HideInInspector] public int maxWarpTimes;
    [HideInInspector] public bool isLimited;

    private int _warpTimes;
    int warpTimes
    {
        get => _warpTimes;
        set
        {
            _warpTimes = value;
            if (gameSceneUI != null) gameSceneUI.ChangeActionCount(ActionKind.N_Warp, value);
        }
    }

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
        if (playerMovement.IsLanding() && playerMovement.rb.velocity.y <= 5f) warpTimes = maxWarpTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (warpTimes <= 0) 
        {
            isLimited = true;
            return;
        }
        warpTimes --;
        isLimited = false;

        foreach (var action in warpActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Warp();
        }
    }

    public void Recure()
    {
        warpTimes = maxWarpTimes;
    }

    public void ChangeMaxTimes(int times)
    {
        if (times < maxWarpTimes) warpTimes -= maxWarpTimes - times;

        maxWarpTimes = times;

        if (gameSceneUI == null) return;
        if (times == 0) gameSceneUI.VisibleActionCount(ActionKind.N_Warp, false);
        else gameSceneUI.VisibleActionCount(ActionKind.N_Warp, true);
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
