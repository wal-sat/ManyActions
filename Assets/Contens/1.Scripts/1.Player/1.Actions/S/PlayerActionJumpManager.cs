using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionJumpBase[] jumpActions;
    [SerializeField] float RESTRICTE_JUMP_SPEED;

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
        if (playerMovement.IsLanding() && playerMovement.rb.velocity.y <= 5f) _jumpTimes = maxJumpTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (playerMovement.rb.velocity.y > RESTRICTE_JUMP_SPEED) return;
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

    public void ChangeMaxTimes(int times)
    {
        if (times < maxJumpTimes) _jumpTimes -= maxJumpTimes - times;

        maxJumpTimes = times;
    }
}



