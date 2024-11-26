using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpKind { Jump, BigJump, FrontJump, BackJump }

public class PlayerActionJumpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpBase[] jumpActions;
    [SerializeField] int MAX_JUMP_TIMES;

    private bool _isLanding;
    private bool _wasLanding;
    private int _jumpTimes;

    private void Start()
    {
        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            action.init = Init;
        }
    }

    private void Update()
    {
        _wasLanding = _isLanding;
        _isLanding = playerMovement.IsLanding();

        if (_isLanding && !_wasLanding) _jumpTimes = MAX_JUMP_TIMES;
        else if (!_isLanding && _wasLanding) _jumpTimes = MAX_JUMP_TIMES - 1; 
    }

    private void Init(InputKind inputKind, JumpKind jumpKind)
    {
        if (_jumpTimes <= 0) return;
        _jumpTimes --;

        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            if (action.jumpKind == jumpKind && action.assignedInput == inputKind) action.Jump();
        }
    }

    public void OnWallKickJump()
    {
        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            action.wasJumped = false;
        }
    }
}


