using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionKind 
{
    LR_Swap, LR_Kick, LR_Interact, LeftRight_Accelerate, LeftRight_Decelerate, Up_Grab, Down_Crouch, 
    S_Jump, S_BigJump, S_FrontJump, S_BackJump, S_GoDown, S_DoubleJump, S_InfiniteJump,
    E_Hover, E_UpBlink, E_Blink, E_StopHover, E_Swoop, E_DoubleBlink, E_InfiniteBlink,
    W_, W_Up, W_Left, W_Right, W_Down, W_Double, W_Infinite,
    N_Invincible, N_UpWarp, N_Warp, N_DoubleWarp, N_InfiniteWarp,
}

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] GameSceneOnPlayInput gameSceneOnPlayInput;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] PlayerActionBlinkManager playerActionBlinkManager;
    [SerializeField] PlayerActionWarpManager playerActionWarpManager;
    [SerializeField] PlayerActionBase[] playerActions;
    [SerializeField] private float INPUT_BLOCK_TIME;

    private Dictionary<ActionKind, bool> _availableActions;

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
    private bool _onR1;
    private bool _onL1;
    private bool _onR2;
    private bool _onL2;

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
    private bool _onR1Past;
    private bool _onL1Past;
    private bool _onR2Past;
    private bool _onL2Past;

    private bool _SBlock;
    private bool _EBlock;
    private bool _WBlock;

    private float _STimer;
    private float _ETimer;
    private float _WTimer;

    [HideInInspector] public bool NLogicalDisjunction;
    [HideInInspector] public bool NBlock;
    [HideInInspector] public bool NBlockPast;

    private void Awake()
    {
        playerMovement.OnSwapCallback = SwapInActionCallback;
        playerActionJumpManager.IsCoolTime = IsCoolTime;
        playerActionBlinkManager.IsCoolTime = IsCoolTime;
    }

    public void Initialize()
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            action.Initialize();
        }

        AllBoolFalse();
    }
    //PlayerManagerからFixedUpdateで呼ばれる
    public void ActionUpdate()
    {
        if (playerMovement.IsLanding()) Recure();

        TracePast();
        
        InputAdjustment();

        Action();
    }

    private void Recure()
    {
        playerActionJumpManager.Recure();
        playerActionBlinkManager.Recure();
        playerActionWarpManager.Recure();
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
        _onR1Past = _onR1;
        _onL1Past = _onL1;
        _onR2Past = _onR2;
        _onL2Past = _onL2;

        NBlockPast = NBlock;
    }
    private void InputAdjustment()
    {
        AllBoolFalse();

        _onUp = (gameSceneOnPlayInput.onUp | gameSceneOnPlayInput.onS_Up | gameSceneOnPlayInput.onE_Up | gameSceneOnPlayInput.onW_Up | gameSceneOnPlayInput.onN_Up);
        _onLeft = (gameSceneOnPlayInput.onLeft | gameSceneOnPlayInput.onS_Left | gameSceneOnPlayInput.onE_Left | gameSceneOnPlayInput.onW_Left | gameSceneOnPlayInput.onN_Left);
        _onRight = (gameSceneOnPlayInput.onRight | gameSceneOnPlayInput.onS_Right | gameSceneOnPlayInput.onE_Right | gameSceneOnPlayInput.onW_Right | gameSceneOnPlayInput.onN_Right);
        _onDown = (gameSceneOnPlayInput.onDown | gameSceneOnPlayInput.onS_Down | gameSceneOnPlayInput.onE_Down | gameSceneOnPlayInput.onW_Down | gameSceneOnPlayInput.onN_Down);

        if ((!gameSceneOnPlayInput.onS_Up && gameSceneOnPlayInput.onS_UpPast) || (!gameSceneOnPlayInput.onS_Left && gameSceneOnPlayInput.onS_LeftPast) || (!gameSceneOnPlayInput.onS_Right && gameSceneOnPlayInput.onS_RightPast) || (!gameSceneOnPlayInput.onS_Down && gameSceneOnPlayInput.onS_DownPast))
        {
            _SBlock = true;
            _STimer = 0;
        }
        if ((!gameSceneOnPlayInput.onE_Up && gameSceneOnPlayInput.onE_UpPast) || (!gameSceneOnPlayInput.onE_Left && gameSceneOnPlayInput.onE_LeftPast) || (!gameSceneOnPlayInput.onE_Right && gameSceneOnPlayInput.onE_RightPast) || (!gameSceneOnPlayInput.onE_Down && gameSceneOnPlayInput.onE_DownPast))
        {
            _EBlock = true;
            _ETimer = 0;
        }
        if ((!gameSceneOnPlayInput.onW_Up && gameSceneOnPlayInput.onW_UpPast) || (!gameSceneOnPlayInput.onW_Left && gameSceneOnPlayInput.onW_LeftPast) || (!gameSceneOnPlayInput.onW_Right && gameSceneOnPlayInput.onW_RightPast) || (!gameSceneOnPlayInput.onW_Down && gameSceneOnPlayInput.onW_DownPast))
        {
            _WBlock = true;
            _WTimer = 0;
        }

        if (gameSceneOnPlayInput.onS_Up && _availableActions[ActionKind.S_BigJump]) _onS_Up = true;
        else if (gameSceneOnPlayInput.onS_Left && ( _availableActions[ActionKind.S_FrontJump] || _availableActions[ActionKind.S_BackJump] ) ) _onS_Left = true;
        else if (gameSceneOnPlayInput.onS_Right && ( _availableActions[ActionKind.S_FrontJump] || _availableActions[ActionKind.S_BackJump] ) ) _onS_Right = true;
        else if (gameSceneOnPlayInput.onS_Down && _availableActions[ActionKind.S_GoDown]) _onS_Down = true;
        else if (gameSceneOnPlayInput.onS && _availableActions[ActionKind.S_Jump]) _onS = true;
        if (_SBlock)
        {
            _onS = false;
            _STimer += Time.deltaTime;
            if (_STimer > INPUT_BLOCK_TIME) _SBlock = false;
        }

        if (gameSceneOnPlayInput.onE_Up && _availableActions[ActionKind.E_UpBlink]) _onE_Up = true;
        else if (gameSceneOnPlayInput.onE_Left && ( _availableActions[ActionKind.E_Blink] || _availableActions[ActionKind.E_StopHover] ) ) _onE_Left = true;
        else if (gameSceneOnPlayInput.onE_Right && ( _availableActions[ActionKind.E_Blink] || _availableActions[ActionKind.E_StopHover] ) ) _onE_Right = true;
        else if (gameSceneOnPlayInput.onE_Down && _availableActions[ActionKind.E_Swoop]) _onE_Down = true;
        else if (gameSceneOnPlayInput.onE && _availableActions[ActionKind.E_Hover]) _onE = true;
        if (_EBlock)
        {
            _onE = false;
            _ETimer += Time.deltaTime;
            if (_ETimer > INPUT_BLOCK_TIME) _EBlock = false;
        }

        if (gameSceneOnPlayInput.onW_Up && _availableActions[ActionKind.W_Up]) _onW_Up = true;
        else if (gameSceneOnPlayInput.onW_Left && ( _availableActions[ActionKind.W_Left] || _availableActions[ActionKind.W_Right] ) ) _onW_Left = true;
        else if (gameSceneOnPlayInput.onW_Right && ( _availableActions[ActionKind.W_Left] || _availableActions[ActionKind.W_Right] ) ) _onW_Right = true;
        else if (gameSceneOnPlayInput.onW_Down && _availableActions[ActionKind.W_Down]) _onW_Down = true;
        else if (gameSceneOnPlayInput.onW && _availableActions[ActionKind.W_]) _onW = true;
        if (_WBlock)
        {
            _onW = false;
            _WTimer += Time.deltaTime;
            if (_WTimer > INPUT_BLOCK_TIME) _WBlock = false;
        }

        if (gameSceneOnPlayInput.onN_Up && _availableActions[ActionKind.N_UpWarp]) _onN_Up = true;
        else if (gameSceneOnPlayInput.onN_Left && ( _availableActions[ActionKind.N_UpWarp] || _availableActions[ActionKind.N_Warp] ) ) _onN_Left = true;
        else if (gameSceneOnPlayInput.onN_Right && ( _availableActions[ActionKind.N_UpWarp] || _availableActions[ActionKind.N_Warp] ) ) _onN_Right = true;
        else if (gameSceneOnPlayInput.onN_Down && ( _availableActions[ActionKind.N_UpWarp] || _availableActions[ActionKind.N_Warp] ) ) _onN_Down = true;
        else if (gameSceneOnPlayInput.onN && ( _availableActions[ActionKind.N_UpWarp] || _availableActions[ActionKind.N_Invincible] ) ) _onN = true;
        NLogicalDisjunction = (_onN || _onN_Up || _onN_Left || _onN_Right || _onN_Down);
        if (_onN_Up && !NBlock) NBlock = true;
        if (NBlock && !NLogicalDisjunction) NBlock = false;

        _onL1 = gameSceneOnPlayInput.onL1;
        _onR1 = gameSceneOnPlayInput.onR1;
        _onL2 = gameSceneOnPlayInput.onL2;
        _onR2 = gameSceneOnPlayInput.onR2;
    }
    private void Action()
    {
        //ーーーNeutralーーー
        if (_onUp && !_onUpPast) CallInitAction(InputKind.Up);
        if (_onDown && !_onDownPast) CallInitAction(InputKind.Down);
        if (_onLeft && !_onLeftPast) CallInitAction(InputKind.Left);
        if (_onRight && !_onRightPast) CallInitAction(InputKind.Right);
        if (_onL1 && !_onL1Past) CallInitAction(InputKind.L1);
        if (_onR1 && !_onR1Past) CallInitAction(InputKind.R1);
        if (_onL2 && !_onL2Past) CallInitAction(InputKind.L2);
        if (_onR2 && !_onR2Past) CallInitAction(InputKind.R2);
        
        if (_onUp && _onUpPast) CallInAction(InputKind.Up);
        if (_onDown && _onDownPast) CallInAction(InputKind.Down);
        if (_onLeft && _onLeftPast) CallInAction(InputKind.Left);
        if (_onRight && _onRightPast) CallInAction(InputKind.Right);
        if (_onL1 && _onL1Past) CallInAction(InputKind.L1);
        if (_onR1 && _onR1Past) CallInAction(InputKind.R1);
        if (_onL2 && _onL2Past) CallInAction(InputKind.L2);
        if (_onR2 && _onR2Past) CallInAction(InputKind.R2);
        
        if (!_onUp && _onUpPast) CallEndAction(InputKind.Up);
        if (!_onDown && _onDownPast) CallEndAction(InputKind.Down);
        if (!_onLeft && _onLeftPast) CallEndAction(InputKind.Left);
        if (!_onRight && _onRightPast) CallEndAction(InputKind.Right);
        if (!_onL1 && _onL1Past) CallEndAction(InputKind.L1);
        if (!_onR1 && _onR1Past) CallEndAction(InputKind.R1);
        if (!_onL2 && _onL2Past) CallEndAction(InputKind.L2);
        if (!_onR2 && _onR2Past) CallEndAction(InputKind.R2);

        //ーーーSーーー
        if (_onS && !_onSPast) CallInitAction(InputKind.S);
        if (_onS_Up && !_onS_UpPast) CallInitAction(InputKind.S_Up);
        if (_onS_Down && !_onS_DownPast) CallInitAction(InputKind.S_Down);
        if (_onS_Left && !_onS_LeftPast) CallInitAction(InputKind.S_Left);
        if (_onS_Right && !_onS_RightPast) CallInitAction(InputKind.S_Right);
        
        if (_onS && _onSPast) CallInAction(InputKind.S);
        if (_onS_Up && _onS_UpPast) CallInAction(InputKind.S_Up);
        if (_onS_Down && _onS_DownPast) CallInAction(InputKind.S_Down);
        if (_onS_Left && _onS_LeftPast) CallInAction(InputKind.S_Left);
        if (_onS_Right && _onS_RightPast) CallInAction(InputKind.S_Right);
        
        if (!_onS && _onSPast) CallEndAction(InputKind.S);
        if (!_onS_Up && _onS_UpPast) CallEndAction(InputKind.S_Up);
        if (!_onS_Down && _onS_DownPast) CallEndAction(InputKind.S_Down);
        if (!_onS_Left && _onS_LeftPast) CallEndAction(InputKind.S_Left);
        if (!_onS_Right && _onS_RightPast) CallEndAction(InputKind.S_Right);

        //ーーーEーーー
        if (_onE && !_onEPast) CallInitAction(InputKind.E);
        if (_onE_Up && !_onE_UpPast) CallInitAction(InputKind.E_Up);
        if (_onE_Down && !_onE_DownPast) CallInitAction(InputKind.E_Down);
        if (_onE_Left && !_onE_LeftPast) CallInitAction(InputKind.E_Left);
        if (_onE_Right && !_onE_RightPast) CallInitAction(InputKind.E_Right);
        
        if (_onE && _onEPast) CallInAction(InputKind.E);
        if (_onE_Up && _onE_UpPast) CallInAction(InputKind.E_Up);
        if (_onE_Down && _onE_DownPast) CallInAction(InputKind.E_Down);
        if (_onE_Left && _onE_LeftPast) CallInAction(InputKind.E_Left);
        if (_onE_Right && _onE_RightPast) CallInAction(InputKind.E_Right);
        
        if (!_onE && _onEPast) CallEndAction(InputKind.E);
        if (!_onE_Up && _onE_UpPast) CallEndAction(InputKind.E_Up);
        if (!_onE_Down && _onE_DownPast) CallEndAction(InputKind.E_Down);
        if (!_onE_Left && _onE_LeftPast) CallEndAction(InputKind.E_Left);
        if (!_onE_Right && _onE_RightPast) CallEndAction(InputKind.E_Right);

        //ーーーWーーー
        if (_onW && !_onWPast) CallInitAction(InputKind.W);
        if (_onW_Up && !_onW_UpPast) CallInitAction(InputKind.W_Up);
        if (_onW_Down && !_onW_DownPast) CallInitAction(InputKind.W_Down);
        if (_onW_Left && !_onW_LeftPast) CallInitAction(InputKind.W_Left);
        if (_onW_Right && !_onW_RightPast) CallInitAction(InputKind.W_Right);
        
        if (_onW && _onWPast) CallInAction(InputKind.W);
        if (_onW_Up && _onW_UpPast) CallInAction(InputKind.W_Up);
        if (_onW_Down && _onW_DownPast) CallInAction(InputKind.W_Down);
        if (_onW_Left && _onW_LeftPast) CallInAction(InputKind.W_Left);
        if (_onW_Right && _onW_RightPast) CallInAction(InputKind.W_Right);
        
        if (!_onW && _onWPast) CallEndAction(InputKind.W);
        if (!_onW_Up && _onW_UpPast) CallEndAction(InputKind.W_Up);
        if (!_onW_Down && _onW_DownPast) CallEndAction(InputKind.W_Down);
        if (!_onW_Left && _onW_LeftPast) CallEndAction(InputKind.W_Left);
        if (!_onW_Right && _onW_RightPast) CallEndAction(InputKind.W_Right);

        //ーーーNーーー
        if (_onN && !_onNPast) CallInitAction(InputKind.N);
        if (_onN_Up && !_onN_UpPast) CallInitAction(InputKind.N_Up);
        if (_onN_Down && !_onN_DownPast) CallInitAction(InputKind.N_Down);
        if (_onN_Left && !_onN_LeftPast) CallInitAction(InputKind.N_Left);
        if (_onN_Right && !_onN_RightPast) CallInitAction(InputKind.N_Right);
        
        if (_onN && _onNPast) CallInAction(InputKind.N);
        if (_onN_Up && _onN_UpPast) CallInAction(InputKind.N_Up);
        if (_onN_Down && _onN_DownPast) CallInAction(InputKind.N_Down);
        if (_onN_Left && _onN_LeftPast) CallInAction(InputKind.N_Left);
        if (_onN_Right && _onN_RightPast) CallInAction(InputKind.N_Right);
        
        if (!_onN && _onNPast) CallEndAction(InputKind.N);
        if (!_onN_Up && _onN_UpPast) CallEndAction(InputKind.N_Up);
        if (!_onN_Down && _onN_DownPast) CallEndAction(InputKind.N_Down);
        if (!_onN_Left && _onN_LeftPast) CallEndAction(InputKind.N_Left);
        if (!_onN_Right && _onN_RightPast) CallEndAction(InputKind.N_Right);
    }

    public void CallInitAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.InitAction();
        }
    }
    public void CallInAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.InAction();
        }
    }
    public void CallEndAction(InputKind inputKind)
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.assignedInput == inputKind && action.isEnable) action.EndAction();
        }
    }

    private void AllBoolFalse()
    {
        _onUp = false;
        _onDown = false;
        _onLeft = false;
        _onRight = false;
        _onS = false;
        _onS_Up = false;
        _onS_Down = false;
        _onS_Left = false;
        _onS_Right = false;
        _onE = false;
        _onE_Up = false;
        _onE_Down = false;
        _onE_Left = false;
        _onE_Right = false;
        _onW = false;
        _onW_Up = false;
        _onW_Down = false;
        _onW_Left = false;
        _onW_Right = false;
        _onN = false;
        _onN_Up = false;
        _onN_Down = false;
        _onN_Left = false;
        _onN_Right = false;
        _onR1 = false;
        _onL1 = false;
        _onR2 = false;
        _onL2 = false;
    }

    public void SetAvailableActions(AcquireActionData acquireActionData)
    {
        _availableActions = new Dictionary<ActionKind, bool>( acquireActionData.availableActions );

        foreach (var availableAction in _availableActions)
        {
            foreach (var action in playerActions)
            {
                if (action == null) continue;
                if (action.actionKind == availableAction.Key)
                {
                    if (action.isEnable && !availableAction.Value) action.Initialize();
                    action.isEnable = availableAction.Value;
                }
            }
        }


        Recure();
    }
    public void EnableAction(ActionKind actionKind, bool isEnable)
    {
        if (_availableActions[actionKind] == isEnable) return;

        _availableActions[actionKind] = isEnable;

        foreach (var action in playerActions)
        {
            if (action == null) continue;
            if (action.actionKind == actionKind)
            {
                if (action.isEnable && !isEnable) action.Initialize();
                action.isEnable = isEnable;
            }
        }

        playerActionJumpManager.ChangeJumpTimes();
        playerActionBlinkManager.ChangeBlinkTimes();
        playerActionWarpManager.ChangeWarpTimes();
    }

    private void SwapInActionCallback()
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;

            if (action.isEnable && action.isAction) action.SwapInAction();
        }
    }
    private bool IsCoolTime()
    {
        foreach (var action in playerActions)
        {
            if (action == null) continue;
            if (action.isCoolTime) return true;
        }
        return false;
    }   
}
