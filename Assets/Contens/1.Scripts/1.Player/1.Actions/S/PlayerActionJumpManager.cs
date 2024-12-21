using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpBase[] jumpActions;

    [HideInInspector] public int maxJumpTimes;

    private bool _isLanding;
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
        _isLanding = playerMovement.IsLanding();

        if (_isLanding) _jumpTimes = maxJumpTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (_jumpTimes <= 0) return;
        _jumpTimes --;

        foreach (var action in jumpActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Jump();
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

    public void Recure()
    {
        _jumpTimes = maxJumpTimes;
    }
}


