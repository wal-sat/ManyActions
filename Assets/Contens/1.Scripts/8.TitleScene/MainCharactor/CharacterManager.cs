using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimationManager playerAnimationManager;
    [SerializeField] PlayerActionManager playerActionManager;
    [SerializeField] PlayerPreventStuck playerPreventStuck;
    [SerializeField] AcquireActionData acquireActionData;

    private List<ActionInfo> _actionList = new List<ActionInfo>();
    private float _dieTime;

    private float _timer;

    public void Init(List<ActionInfo> actionList, float dieTime, bool isFacingRight)
    {
        _actionList = actionList;
        _dieTime = dieTime;
        _timer = 0;

        playerMovement.Initialize(isFacingRight);
        playerAnimationManager.Initialize(isFacingRight);
        playerActionManager.Initialize();
        playerPreventStuck.Initialize();

        playerAnimationManager.AnimationUpdate(false);

        playerActionManager.SetAvailableActions(acquireActionData);
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        
        foreach (var actionInfo in _actionList)
        {
            actionInfo.onPast = actionInfo.on;

            if (actionInfo.InitTime < _timer && _timer < actionInfo.InitTime + actionInfo.DurationTime) actionInfo.on = true;
            else actionInfo.on = false;

            if (actionInfo.on && !actionInfo.onPast) playerActionManager.CallInitAction(actionInfo.inputKind);
            else if (actionInfo.on && actionInfo.onPast) playerActionManager.CallInAction(actionInfo.inputKind);
            else if (!actionInfo.on && actionInfo.onPast) playerActionManager.CallEndAction(actionInfo.inputKind);
        }


        playerAnimationManager.AnimationUpdate(true);
        
        playerMovement.MovementUpdate();
        playerPreventStuck.PreventStuckUpdate();
        
        if (_timer > _dieTime) Destroy(this.gameObject);
    }
}
