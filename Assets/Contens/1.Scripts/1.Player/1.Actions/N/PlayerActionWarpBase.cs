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

    public Action<InputKind, ActionKind> init;

    public override void InitAction()
    {
        init(assignedInput, actionKind);
    }

    public virtual void Warp()
    {
        ;
    }
}
