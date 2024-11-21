using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerActionWarpBase : PlayerActionBase
{
    [SerializeField] public PlayerActionManager playerActionManager;
    [SerializeField] public PlayerActionWarpManager playerActionWarpManager;

    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public WarpKind warpKind;

    public Action<InputKind, WarpKind> init;

    public override void InitAction()
    {
        init(assignedInput, warpKind);
    }

    public virtual void Warp()
    {
        ;
    }
}
