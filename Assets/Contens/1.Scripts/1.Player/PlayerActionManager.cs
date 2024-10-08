using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] PlayerActionBase[] playerActions;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private float INPUT_BLOCK_TIME;

    private bool _onUp;
    private bool _onDown;
    private bool _onLeft;
    private bool _onRight;
    private bool _onS;
    private bool _onS_Up;
    private bool _onS_Down;
    private bool _onS_Left;
    private bool _onS_Right;
    private bool _onE;
    private bool _onE_Up;
    private bool _onE_Down;
    private bool _onE_Left;
    private bool _onE_Right;
    private bool _onW;
    private bool _onW_Up;
    private bool _onW_Down;
    private bool _onW_Left;
    private bool _onW_Right;
    private bool _onN;
    private bool _onN_Up;
    private bool _onN_Down;
    private bool _onN_Left;
    private bool _onN_Right;
    private bool _onR;
    private bool _onL;

    private bool _onUpPast;
    private bool _onDownPast;
    private bool _onLeftPast;
    private bool _onRightPast;
    private bool _onSPast;
    private bool _onS_UpPast;
    private bool _onS_DownPast;
    private bool _onS_LeftPast;
    private bool _onS_RightPast;
    private bool _onEPast;
    private bool _onE_UpPast;
    private bool _onE_DownPast;
    private bool _onE_LeftPast;
    private bool _onE_RightPast;
    private bool _onWPast;
    private bool _onW_UpPast;
    private bool _onW_DownPast;
    private bool _onW_LeftPast;
    private bool _onW_RightPast;
    private bool _onNPast;
    private bool _onN_UpPast;
    private bool _onN_DownPast;
    private bool _onN_LeftPast;
    private bool _onN_RightPast;
    private bool _onRPast;
    private bool _onLPast;

    private bool _SBlock;
    private bool _EBlock;
    private bool _WBlock;
    [HideInInspector] public bool NBlock;

    private float _STimer;
    private float _ETimer;
    private float _WTimer;

    private void FixedUpdate()
    {
        playerInput.PlayerInputUpdate();

        TracePast();
        
        InputAdjustment();

        Action();
    }

    private void TracePast()
    {
        _onUpPast = _onUp;
        _onDownPast = _onDown;
        _onLeftPast = _onLeft;
        _onRightPast = _onRight;
        _onSPast = _onS;
        _onS_UpPast = _onS_Up;
        _onS_DownPast = _onS_Down;
        _onS_LeftPast = _onS_Left;
        _onS_RightPast = _onS_Right;
        _onEPast = _onE;
        _onE_UpPast = _onE_Up;
        _onE_DownPast = _onE_Down;
        _onE_LeftPast = _onE_Left;
        _onE_RightPast = _onE_Right;
        _onWPast = _onW;
        _onW_UpPast = _onW_Up;
        _onW_DownPast = _onW_Down;
        _onW_LeftPast = _onW_Left;
        _onW_RightPast = _onW_Right;
        _onNPast = _onN;
        _onN_UpPast = _onN_Up;
        _onN_DownPast = _onN_Down;
        _onN_LeftPast = _onN_Left;
        _onN_RightPast = _onN_Right;
        _onRPast = _onR;
        _onLPast = _onL;
    }

    private void InputAdjustment()
    {
        _onUp = (playerInput.onUp | playerInput.onS_Up | playerInput.onE_Up | playerInput.onW_Up | playerInput.onN_Up);
        _onLeft = (playerInput.onLeft | playerInput.onS_Left | playerInput.onE_Left | playerInput.onW_Left | playerInput.onN_Left);
        _onRight = (playerInput.onRight | playerInput.onS_Right | playerInput.onE_Right | playerInput.onW_Right | playerInput.onN_Right);
        _onDown = (playerInput.onDown | playerInput.onS_Down | playerInput.onE_Down | playerInput.onW_Down | playerInput.onN_Down);

        if ((!playerInput.onS_Up && playerInput.onS_UpPast) || (!playerInput.onS_Left && playerInput.onS_LeftPast) || (!playerInput.onS_Right && playerInput.onS_RightPast) || (!playerInput.onS_Down && playerInput.onS_DownPast))
        {
            _SBlock = true;
            _STimer = 0;
        }
        if ((!playerInput.onE_Up && playerInput.onE_UpPast) || (!playerInput.onE_Left && playerInput.onE_LeftPast) || (!playerInput.onE_Right && playerInput.onE_RightPast) || (!playerInput.onE_Down && playerInput.onE_DownPast))
        {
            _EBlock = true;
            _ETimer = 0;
        }
        if ((!playerInput.onW_Up && playerInput.onW_UpPast) || (!playerInput.onW_Left && playerInput.onW_LeftPast) || (!playerInput.onW_Right && playerInput.onW_RightPast) || (!playerInput.onW_Down && playerInput.onW_DownPast))
        {
            _WBlock = true;
            _WTimer = 0;
        }

        _onS = playerInput.onS;
        if (_SBlock)
        {
            _onS = false;
            _STimer += Time.deltaTime;
            if (_STimer > INPUT_BLOCK_TIME) _SBlock = false;
        }
        _onS_Up = playerInput.onS_Up;
        _onS_Left = playerInput.onS_Left;
        _onS_Right = playerInput.onS_Right;
        _onS_Down = playerInput.onS_Down;

        _onE = playerInput.onE;
        if (_EBlock)
        {
            _onE = false;
            _ETimer += Time.deltaTime;
            if (_ETimer > INPUT_BLOCK_TIME) _EBlock = false;
        }
        _onE_Up = playerInput.onE_Up;
        _onE_Left = playerInput.onE_Left;
        _onE_Right = playerInput.onE_Right;
        _onE_Down = playerInput.onE_Down;

        _onW = playerInput.onW;
        if (_WBlock)
        {
            _onW = false;
            _WTimer += Time.deltaTime;
            if (_WTimer > INPUT_BLOCK_TIME) _WBlock = false;
        }
        _onW_Up = playerInput.onW_Up;
        _onW_Left = playerInput.onW_Left;
        _onW_Right = playerInput.onW_Right;
        _onW_Down = playerInput.onW_Down;

        _onN = playerInput.onN;
        _onN_Up = playerInput.onN_Up;
        _onN_Left = playerInput.onN_Left;
        _onN_Right = playerInput.onN_Right;
        _onN_Down = playerInput.onN_Down;
        if (!playerInput.onN_Up && playerInput.onN_UpPast)
        {
            if (playerInput.onN || playerInput.onN_Up || playerInput.onN_Left || playerInput.onN_Right || playerInput.onN_Down) NBlock = true;
        }
        if (NBlock)
        {
            if (!playerInput.onN && !playerInput.onN_Up && !playerInput.onN_Left && !playerInput.onN_Right && !playerInput.onN_Down) NBlock = false;
        }

        _onL = playerInput.onL;
        _onR = playerInput.onR;
    }

    private void Action()
    {
        //ーーーNeutralーーー
        if (_onUp && !_onUpPast) CallInitAction(InputKind.Up);
        else if (_onUp && _onUpPast) CallInAction(InputKind.Up);
        else if (!_onUp && _onUpPast) CallEndAction(InputKind.Up);

        if (_onDown && !_onDownPast) CallInitAction(InputKind.Down);
        else if (_onDown && _onDownPast) CallInAction(InputKind.Down);
        else if (!_onDown && _onDownPast) CallEndAction(InputKind.Down);

        if (_onLeft && !_onLeftPast) CallInitAction(InputKind.Left);
        else if (_onLeft && _onLeftPast) CallInAction(InputKind.Left);
        else if (!_onLeft && _onLeftPast) CallEndAction(InputKind.Left);

        if (_onRight && !_onRightPast) CallInitAction(InputKind.Right);
        else if (_onRight && _onRightPast) CallInAction(InputKind.Right);
        else if (!_onRight && _onRightPast) CallEndAction(InputKind.Right);

        if (_onL && !_onLPast) CallInitAction(InputKind.L);
        else if (_onL && _onLPast) CallInAction(InputKind.L);
        else if (!_onL && _onLPast) CallEndAction(InputKind.L);

        if (_onR && !_onRPast) CallInitAction(InputKind.R);
        else if (_onR && _onRPast) CallInAction(InputKind.R);
        else if (!_onR && _onRPast) CallEndAction(InputKind.R);

        //ーーーSーーー
        if (_onS && !_onSPast) CallInitAction(InputKind.S);
        else if (_onS && _onSPast) CallInAction(InputKind.S);
        else if (!_onS && _onSPast) CallEndAction(InputKind.S);

        if (_onS_Up && !_onS_UpPast) CallInitAction(InputKind.S_Up);
        else if (_onS_Up && _onS_UpPast) CallInAction(InputKind.S_Up);
        else if (!_onS_Up && _onS_UpPast) CallEndAction(InputKind.S_Up);

        if (_onS_Down && !_onS_DownPast) CallInitAction(InputKind.S_Down);
        else if (_onS_Down && _onS_DownPast) CallInAction(InputKind.S_Down);
        else if (!_onS_Down && _onS_DownPast) CallEndAction(InputKind.S_Down);

        if (_onS_Left && !_onS_LeftPast) CallInitAction(InputKind.S_Left);
        else if (_onS_Left && _onS_LeftPast) CallInAction(InputKind.S_Left);
        else if (!_onS_Left && _onS_LeftPast) CallEndAction(InputKind.S_Left);

        if (_onS_Right && !_onS_RightPast) CallInitAction(InputKind.S_Right);
        else if (_onS_Right && _onS_RightPast) CallInAction(InputKind.S_Right);
        else if (!_onS_Right && _onS_RightPast) CallEndAction(InputKind.S_Right);

        //ーーーEーーー
        if (_onE && !_onEPast) CallInitAction(InputKind.E);
        else if (_onE && _onEPast) CallInAction(InputKind.E);
        else if (!_onE && _onEPast) CallEndAction(InputKind.E);

        if (_onE_Up && !_onE_UpPast) CallInitAction(InputKind.E_Up);
        else if (_onE_Up && _onE_UpPast) CallInAction(InputKind.E_Up);
        else if (!_onE_Up && _onE_UpPast) CallEndAction(InputKind.E_Up);

        if (_onE_Down && !_onE_DownPast) CallInitAction(InputKind.E_Down);
        else if (_onE_Down && _onE_DownPast) CallInAction(InputKind.E_Down);
        else if (!_onE_Down && _onE_DownPast) CallEndAction(InputKind.E_Down);

        if (_onE_Left && !_onE_LeftPast) CallInitAction(InputKind.E_Left);
        else if (_onE_Left && _onE_LeftPast) CallInAction(InputKind.E_Left);
        else if (!_onE_Left && _onE_LeftPast) CallEndAction(InputKind.E_Left);

        if (_onE_Right && !_onE_RightPast) CallInitAction(InputKind.E_Right);
        else if (_onE_Right && _onE_RightPast) CallInAction(InputKind.E_Right);
        else if (!_onE_Right && _onE_RightPast) CallEndAction(InputKind.E_Right);

        //ーーーWーーー
        if (_onW && !_onWPast) CallInitAction(InputKind.W);
        else if (_onW && _onWPast) CallInAction(InputKind.W);
        else if (!_onW && _onWPast) CallEndAction(InputKind.W);

        if (_onW_Up && !_onW_UpPast) CallInitAction(InputKind.W_Up);
        else if (_onW_Up && _onW_UpPast) CallInAction(InputKind.W_Up);
        else if (!_onW_Up && _onW_UpPast) CallEndAction(InputKind.W_Up);

        if (_onW_Down && !_onW_DownPast) CallInitAction(InputKind.W_Down);
        else if (_onW_Down && _onW_DownPast) CallInAction(InputKind.W_Down);
        else if (!_onW_Down && _onW_DownPast) CallEndAction(InputKind.W_Down);

        if (_onW_Left && !_onW_LeftPast) CallInitAction(InputKind.W_Left);
        else if (_onW_Left && _onW_LeftPast) CallInAction(InputKind.W_Left);
        else if (!_onW_Left && _onW_LeftPast) CallEndAction(InputKind.W_Left);

        if (_onW_Right && !_onW_RightPast) CallInitAction(InputKind.W_Right);
        else if (_onW_Right && _onW_RightPast) CallInAction(InputKind.W_Right);
        else if (!_onW_Right && _onW_RightPast) CallEndAction(InputKind.W_Right);

        //ーーーNーーー
        if (_onN && !_onNPast) CallInitAction(InputKind.N);
        else if (_onN && _onNPast) CallInAction(InputKind.N);
        else if (!_onN && _onNPast) CallEndAction(InputKind.N);

        if (_onN_Up && !_onN_UpPast) CallInitAction(InputKind.N_Up);
        else if (_onN_Up && _onN_UpPast) CallInAction(InputKind.N_Up);
        else if (!_onN_Up && _onN_UpPast) CallEndAction(InputKind.N_Up);

        if (_onN_Down && !_onN_DownPast) CallInitAction(InputKind.N_Down);
        else if (_onN_Down && _onN_DownPast) CallInAction(InputKind.N_Down);
        else if (!_onN_Down && _onN_DownPast) CallEndAction(InputKind.N_Down);

        if (_onN_Left && !_onN_LeftPast) CallInitAction(InputKind.N_Left);
        else if (_onN_Left && _onN_LeftPast) CallInAction(InputKind.N_Left);
        else if (!_onN_Left && _onN_LeftPast) CallEndAction(InputKind.N_Left);

        if (_onN_Right && !_onN_RightPast) CallInitAction(InputKind.N_Right);
        else if (_onN_Right && _onN_RightPast) CallInAction(InputKind.N_Right);
        else if (!_onN_Right && _onN_RightPast) CallEndAction(InputKind.N_Right);
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
