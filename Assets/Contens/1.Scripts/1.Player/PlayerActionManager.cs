using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerActionBase[] playerActions;
    [SerializeField] PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        playerInput.PlayerInputUpdate();

        Action();
    }

    private void Action()
    {
        //ーーーNeutralーーー
        if (playerInput.onUp && !playerInput.onUpPast) CallInitAction(InputKind.Up);
        else if (playerInput.onUp && playerInput.onUpPast) CallInAction(InputKind.Up);
        else if (!playerInput.onUp && playerInput.onUpPast) CallEndAction(InputKind.Up);

        if (playerInput.onDown && !playerInput.onDownPast) CallInitAction(InputKind.Down);
        else if (playerInput.onDown && playerInput.onDownPast) CallInAction(InputKind.Down);
        else if (!playerInput.onDown && playerInput.onDownPast) CallEndAction(InputKind.Down);

        if (playerInput.onLeft && !playerInput.onLeftPast) CallInitAction(InputKind.Left);
        else if (playerInput.onLeft && playerInput.onLeftPast) CallInAction(InputKind.Left);
        else if (!playerInput.onLeft && playerInput.onLeftPast) CallEndAction(InputKind.Left);

        if (playerInput.onRight && !playerInput.onRightPast) CallInitAction(InputKind.Right);
        else if (playerInput.onRight && playerInput.onRightPast) CallInAction(InputKind.Right);
        else if (!playerInput.onRight && playerInput.onRightPast) CallEndAction(InputKind.Right);

        if (playerInput.onL && !playerInput.onLPast) CallInitAction(InputKind.L);
        else if (playerInput.onL && playerInput.onLPast) CallInAction(InputKind.L);
        else if (!playerInput.onL && playerInput.onLPast) CallEndAction(InputKind.L);

        if (playerInput.onR && !playerInput.onRPast) CallInitAction(InputKind.R);
        else if (playerInput.onR && playerInput.onRPast) CallInAction(InputKind.R);
        else if (!playerInput.onR && playerInput.onRPast) CallEndAction(InputKind.R);

        //ーーーSーーー
        if (playerInput.onS && !playerInput.onSPast) CallInitAction(InputKind.S);
        else if (playerInput.onS && playerInput.onSPast) CallInAction(InputKind.S);
        else if (!playerInput.onS && playerInput.onSPast) CallEndAction(InputKind.S);

        if (playerInput.onS_Up && !playerInput.onS_UpPast) CallInitAction(InputKind.S_Up);
        else if (playerInput.onS_Up && playerInput.onS_UpPast) CallInAction(InputKind.S_Up);
        else if (!playerInput.onS_Up && playerInput.onS_UpPast) CallEndAction(InputKind.S_Up);

        if (playerInput.onS_Down && !playerInput.onS_DownPast) CallInitAction(InputKind.S_Down);
        else if (playerInput.onS_Down && playerInput.onS_DownPast) CallInAction(InputKind.S_Down);
        else if (!playerInput.onS_Down && playerInput.onS_DownPast) CallEndAction(InputKind.S_Down);

        if (playerInput.onS_Left && !playerInput.onS_LeftPast) CallInitAction(InputKind.S_Left);
        else if (playerInput.onS_Left && playerInput.onS_LeftPast) CallInAction(InputKind.S_Left);
        else if (!playerInput.onS_Left && playerInput.onS_LeftPast) CallEndAction(InputKind.S_Left);

        if (playerInput.onS_Right && !playerInput.onS_RightPast) CallInitAction(InputKind.S_Right);
        else if (playerInput.onS_Right && playerInput.onS_RightPast) CallInAction(InputKind.S_Right);
        else if (!playerInput.onS_Right && playerInput.onS_RightPast) CallEndAction(InputKind.S_Right);

        //ーーーEーーー
        if (playerInput.onE && !playerInput.onEPast) CallInitAction(InputKind.E);
        else if (playerInput.onE && playerInput.onEPast) CallInAction(InputKind.E);
        else if (!playerInput.onE && playerInput.onEPast) CallEndAction(InputKind.E);

        if (playerInput.onE_Up && !playerInput.onE_UpPast) CallInitAction(InputKind.E_Up);
        else if (playerInput.onE_Up && playerInput.onE_UpPast) CallInAction(InputKind.E_Up);
        else if (!playerInput.onE_Up && playerInput.onE_UpPast) CallEndAction(InputKind.E_Up);

        if (playerInput.onE_Down && !playerInput.onE_DownPast) CallInitAction(InputKind.E_Down);
        else if (playerInput.onE_Down && playerInput.onE_DownPast) CallInAction(InputKind.E_Down);
        else if (!playerInput.onE_Down && playerInput.onE_DownPast) CallEndAction(InputKind.E_Down);

        if (playerInput.onE_Left && !playerInput.onE_LeftPast) CallInitAction(InputKind.E_Left);
        else if (playerInput.onE_Left && playerInput.onE_LeftPast) CallInAction(InputKind.E_Left);
        else if (!playerInput.onE_Left && playerInput.onE_LeftPast) CallEndAction(InputKind.E_Left);

        if (playerInput.onE_Right && !playerInput.onE_RightPast) CallInitAction(InputKind.E_Right);
        else if (playerInput.onE_Right && playerInput.onE_RightPast) CallInAction(InputKind.E_Right);
        else if (!playerInput.onE_Right && playerInput.onE_RightPast) CallEndAction(InputKind.E_Right);

        //ーーーWーーー
        if (playerInput.onW && !playerInput.onWPast) CallInitAction(InputKind.W);
        else if (playerInput.onW && playerInput.onWPast) CallInAction(InputKind.W);
        else if (!playerInput.onW && playerInput.onWPast) CallEndAction(InputKind.W);

        if (playerInput.onW_Up && !playerInput.onW_UpPast) CallInitAction(InputKind.W_Up);
        else if (playerInput.onW_Up && playerInput.onW_UpPast) CallInAction(InputKind.W_Up);
        else if (!playerInput.onW_Up && playerInput.onW_UpPast) CallEndAction(InputKind.W_Up);

        if (playerInput.onW_Down && !playerInput.onW_DownPast) CallInitAction(InputKind.W_Down);
        else if (playerInput.onW_Down && playerInput.onW_DownPast) CallInAction(InputKind.W_Down);
        else if (!playerInput.onW_Down && playerInput.onW_DownPast) CallEndAction(InputKind.W_Down);

        if (playerInput.onW_Left && !playerInput.onW_LeftPast) CallInitAction(InputKind.W_Left);
        else if (playerInput.onW_Left && playerInput.onW_LeftPast) CallInAction(InputKind.W_Left);
        else if (!playerInput.onW_Left && playerInput.onW_LeftPast) CallEndAction(InputKind.W_Left);

        if (playerInput.onW_Right && !playerInput.onW_RightPast) CallInitAction(InputKind.W_Right);
        else if (playerInput.onW_Right && playerInput.onW_RightPast) CallInAction(InputKind.W_Right);
        else if (!playerInput.onW_Right && playerInput.onW_RightPast) CallEndAction(InputKind.W_Right);

        //ーーーNーーー
        if (playerInput.onN && !playerInput.onNPast) CallInitAction(InputKind.N);
        else if (playerInput.onN && playerInput.onNPast) CallInAction(InputKind.N);
        else if (!playerInput.onN && playerInput.onNPast) CallEndAction(InputKind.N);

        if (playerInput.onN_Up && !playerInput.onN_UpPast) CallInitAction(InputKind.N_Up);
        else if (playerInput.onN_Up && playerInput.onN_UpPast) CallInAction(InputKind.N_Up);
        else if (!playerInput.onN_Up && playerInput.onN_UpPast) CallEndAction(InputKind.N_Up);

        if (playerInput.onN_Down && !playerInput.onN_DownPast) CallInitAction(InputKind.N_Down);
        else if (playerInput.onN_Down && playerInput.onN_DownPast) CallInAction(InputKind.N_Down);
        else if (!playerInput.onN_Down && playerInput.onN_DownPast) CallEndAction(InputKind.N_Down);

        if (playerInput.onN_Left && !playerInput.onN_LeftPast) CallInitAction(InputKind.N_Left);
        else if (playerInput.onN_Left && playerInput.onN_LeftPast) CallInAction(InputKind.N_Left);
        else if (!playerInput.onN_Left && playerInput.onN_LeftPast) CallEndAction(InputKind.N_Left);

        if (playerInput.onN_Right && !playerInput.onN_RightPast) CallInitAction(InputKind.N_Right);
        else if (playerInput.onN_Right && playerInput.onN_RightPast) CallInAction(InputKind.N_Right);
        else if (!playerInput.onN_Right && playerInput.onN_RightPast) CallEndAction(InputKind.N_Right);
    }

    private void CallInitAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.InitAction();
        }
    }
    private void CallInAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.InAction();
        }
    }
    private void CallEndAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.EndAction();
        }
    }
}
