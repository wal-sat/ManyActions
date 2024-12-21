using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/StageActionData", fileName = "StageActionData_")]

public class StageActionData : ScriptableObject
{
    [SerializeField] public int MAX_JUMP_TIMES;
    [SerializeField] public int MAX_BLINK_TIMES;
    [SerializeField] public int MAX_WARP_TIMES;
    [SerializeField] bool LR_Swap;
    [SerializeField] bool LR_Kick;
    [SerializeField] bool LeftRight_Accelerate;
    [SerializeField] bool Up_;
    [SerializeField] bool Down_Crouch; 
    [SerializeField] bool S_Jump;
    [SerializeField] bool S_BigJump;
    [SerializeField] bool S_FrontJump;
    [SerializeField] bool S_BackJump;
    [SerializeField] bool S_GoDown;
    [SerializeField] bool E_Hover;
    [SerializeField] bool E_UpBlink;
    [SerializeField] bool E_Blink;
    [SerializeField] bool E_BackBlink;
    [SerializeField] bool E_Swoop;
    [SerializeField] bool W_;
    [SerializeField] bool W_Up;
    [SerializeField] bool W_Left;
    [SerializeField] bool W_Right;
    [SerializeField] bool W_Down;
    [SerializeField] bool N_Invincible;
    [SerializeField] bool N_UpWarp;
    [SerializeField] bool N_Warp;

    public Dictionary<ActionKind, bool> availableActions = new Dictionary<ActionKind, bool>();

    private void OnEnable()
    {
        availableActions.Add(ActionKind.LR_Swap, LR_Swap);
        availableActions.Add(ActionKind.LR_Kick, LR_Kick);
        availableActions.Add(ActionKind.LeftRight_Accelerate, LeftRight_Accelerate);
        availableActions.Add(ActionKind.Up_, Up_);
        availableActions.Add(ActionKind.Down_Crouch, Down_Crouch);
        availableActions.Add(ActionKind.S_Jump, S_Jump);
        availableActions.Add(ActionKind.S_BigJump, S_BigJump);
        availableActions.Add(ActionKind.S_FrontJump, S_FrontJump);
        availableActions.Add(ActionKind.S_BackJump, S_BackJump);
        availableActions.Add(ActionKind.S_GoDown, S_GoDown);
        availableActions.Add(ActionKind.E_Hover, E_Hover);
        availableActions.Add(ActionKind.E_UpBlink, E_UpBlink);
        availableActions.Add(ActionKind.E_Blink, E_Blink);
        availableActions.Add(ActionKind.E_BackBlink, E_BackBlink);
        availableActions.Add(ActionKind.E_Swoop, E_Swoop);
        availableActions.Add(ActionKind.W_, W_);
        availableActions.Add(ActionKind.W_Up, W_Up);
        availableActions.Add(ActionKind.W_Left, W_Left);
        availableActions.Add(ActionKind.W_Right, W_Right);
        availableActions.Add(ActionKind.W_Down, W_Down);
        availableActions.Add(ActionKind.N_Invincible, N_Invincible);
        availableActions.Add(ActionKind.N_UpWarp, N_UpWarp);
        availableActions.Add(ActionKind.N_Warp, N_Warp);
    }
}
