using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionNManager : MonoBehaviour
{
    [SerializeField] PlayerActionManager playerActionManager;
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
