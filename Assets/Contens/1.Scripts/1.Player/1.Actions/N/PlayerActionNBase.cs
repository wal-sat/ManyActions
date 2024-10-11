using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WarpDirection { up, right, left, down, none }

public class PlayerActionNBase : PlayerActionRequireCoolDownBase
{
    [SerializeField] public PlayerActionManager playerActionManager;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WarpPoint;

    [HideInInspector] public bool onUpWarp;
    [HideInInspector] public bool onUpWarpPast;

    private void FixedUpdate()
    {
        onUpWarpPast = onUpWarp;

        onUpWarp = playerActionManager.NBlock;
    }

    public void InitUpWarp()
    {
        Debug.Log("a");
    }
    public void InUpWarp(WarpDirection warpDirection)
    {
        Debug.Log("a");
    }
    public void EndUpWarp()
    {
        Debug.Log("a");
    }
}
