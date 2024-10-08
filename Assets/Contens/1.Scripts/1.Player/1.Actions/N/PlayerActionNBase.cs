using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActionNBase : PlayerActionRequireCoolDownBase
{
    [SerializeField] public PlayerActionManager playerActionManager;

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
    public void InUpWarp()
    {
        Debug.Log("a");
    }
    public void EndUpWarp()
    {
        Debug.Log("a");
    }
}
