using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionCardInfo
{
    public string actionName;
    public Sprite actionIcon;
}

public class ActionCassetteManager : MonoBehaviour
{
    [SerializeField] ActionCardInfo[] actionCardInfo;

    [HideInInspector] public Dictionary<ActionKind, ActionCardInfo> actionCardInfos = new Dictionary<ActionKind, ActionCardInfo>();

    private List<ActionCassette> _actionCassettes = new List<ActionCassette>();

    private void Awake()
    {
        int index = 0;
        actionCardInfos.Add(ActionKind.LR_Swap, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.LR_Kick, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.LR_Interact, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.LeftRight_Accelerate, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.LeftRight_Decelerate, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.Up_, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.Down_Crouch, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_Jump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_BigJump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_FrontJump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_BackJump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_GoDown, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_DoubleJump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.S_InfiniteJump, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_Hover, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_UpBlink, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_Blink, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_BackBlink, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_Swoop, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_DoubleBlink, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.E_InfiniteBlink, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Up, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Left, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Right, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Down, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Double, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.W_Infinite, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.N_Invincible, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.N_UpWarp, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.N_Warp, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.N_DoubleWarp, actionCardInfo[index++]);
        actionCardInfos.Add(ActionKind.N_InfiniteWarp, actionCardInfo[index++]);
    }

    public void Register(ActionCassette actionCassette)
    {
        _actionCassettes.Add(actionCassette);
    }

    public void Initialize()
    {
        foreach (var actionCassette in _actionCassettes)
        {
            actionCassette.Initialize();
        }
    }
}
