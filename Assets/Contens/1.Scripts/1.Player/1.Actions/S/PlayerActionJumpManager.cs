using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionJumpManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] PlayerActionJumpBase[] jumpActions;
    [SerializeField] float RESTRICTE_JUMP_SPEED;

    [HideInInspector] public int maxJumpTimes;

    private int _jumpTimes;
    int jumpTimes
    {
        get => _jumpTimes;
        set
        {
            _jumpTimes = value;
            if (gameSceneUI != null) gameSceneUI.ChangeActionCount(ActionKind.S_Jump, value);
        }
    }

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
        if (playerMovement.IsLanding() && playerMovement.rb.velocity.y <= 5f) jumpTimes = maxJumpTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (playerMovement.rb.velocity.y > RESTRICTE_JUMP_SPEED) return;
        if (jumpTimes <= 0) return;
        jumpTimes --;

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
        jumpTimes = maxJumpTimes;
    }

    public void ChangeMaxTimes(int times)
    {
        if (times < maxJumpTimes) jumpTimes -= maxJumpTimes - times;

        maxJumpTimes = times;

        if (gameSceneUI == null) return;
        if (times == 0) gameSceneUI.VisibleActionCount(ActionKind.S_Jump, false);
        else gameSceneUI.VisibleActionCount(ActionKind.S_Jump, true);
    }
}



