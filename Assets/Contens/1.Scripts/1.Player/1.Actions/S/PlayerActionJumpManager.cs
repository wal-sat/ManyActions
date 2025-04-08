using System;
using UnityEngine;

public class PlayerActionJumpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpBase[] jumpActions;
    [SerializeField] float RESTRICTE_JUMP_SPEED;

    public Func<bool> IsCoolTime;

    private int _jumpTimes;
    private int _maxJumpTimes;

    private void Awake()
    {
        foreach (var action in jumpActions)
        {
            action.init = Init;
            action.isJumping = IsJumping;
        }
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (playerMovement.rb.velocity.y > RESTRICTE_JUMP_SPEED) return;
        if (IsCoolTime()) return;
        if (_jumpTimes == 0) return;
        if (_jumpTimes != -1) _jumpTimes --;

        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Jump();
        }
    }

    private bool IsJumping(PlayerActionJumpBase selfAction)
    {
        foreach (var action in jumpActions)
        {
            if (action == selfAction) continue;
            if (action.isAction) return true;
        }
        return false;
    }

    public void OnWallKickJump()
    {
        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            action.wasJumped = false;
        }
    }

    public void Recure()
    {
        _maxJumpTimes = SetMaxJumpTimes();
        _jumpTimes = _maxJumpTimes;
    }
    private int SetMaxJumpTimes()
    {
        int maxJumpTimes = 0;
        foreach (var action in jumpActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.S_Jump:
                case ActionKind.S_BigJump:
                case ActionKind.S_FrontJump:
                case ActionKind.S_BackJump:
                    if (action.isEnable && maxJumpTimes == 0) maxJumpTimes = 1;
                    break;
                case ActionKind.S_DoubleJump:
                    if (action.isEnable && ( maxJumpTimes == 0 || maxJumpTimes == 1 ) ) maxJumpTimes = 2;
                    break;
                case ActionKind.S_InfiniteJump:
                    if (action.isEnable) maxJumpTimes = -1;
                    break;
            }
        }
        return maxJumpTimes;
    }

    public void ChangeJumpTimes()
    {
        int maxJumpTimes = 0;
        foreach (var action in jumpActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.S_Jump:
                case ActionKind.S_BigJump:
                case ActionKind.S_FrontJump:
                case ActionKind.S_BackJump:
                    if (action.isEnable && maxJumpTimes == 0) maxJumpTimes = 1;
                    break;
                case ActionKind.S_DoubleJump:
                    if (action.isEnable && ( maxJumpTimes == 0 || maxJumpTimes == 1 ) ) maxJumpTimes = 2;
                    break;
                case ActionKind.S_InfiniteJump:
                    if (action.isEnable) maxJumpTimes = -1;
                    break;
            }
        }

        if (maxJumpTimes == -1) _jumpTimes = -1;
        else _jumpTimes -= _maxJumpTimes - maxJumpTimes;
    }
}



