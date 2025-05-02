using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCassette : MonoBehaviour
{
    enum ActionCassetteState { enable, disable }

    [SerializeField] PlayerManager playerManager;
    [SerializeField] ActionCassetteManager actionCassetteManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] ActionCassetteView actionCassetteView;
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] ActionKind actionKind;
    [SerializeField] ActionCassetteState actionCassetteState;
    [SerializeField] float COOL_TIME = 4f;


    private float _timer;
    private bool _onTimer;
    private bool _isEnable;

    private void Awake()
    {
        _isEnable = true;

        if (actionCassetteManager != null) actionCassetteManager.Register(this);
        stageObjectCollisionArea.triggerEnter = triggerEnter;
    }
    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_TIME)
            {
                _onTimer = false;
                _isEnable = true;
                actionCassetteView.EnableView(true);
            }
        }
    }

    private void triggerEnter()
    {
        if (_isEnable)
        {
            _timer = 0;
            _onTimer = true;
            _isEnable = false;

            actionCassetteView.EnableView(false);

            if (actionCassetteState == ActionCassetteState.enable) 
            {
                if (playerManager != null) playerManager.PlayerActionManager.EnableAction(actionKind, true);
                if (actionCassetteManager != null && gameSceneUI != null) gameSceneUI.MakeActionCard(true, actionCassetteManager.actionCardInfos[actionKind].actionName, actionCassetteManager.actionCardInfos[actionKind].actionIcon);
                
                S_SEManager._instance.Play("s_getActionCassette");
            }
            else if (actionCassetteState == ActionCassetteState.disable)
            {
                if (playerManager != null) playerManager.PlayerActionManager.EnableAction(actionKind, false);
                if (actionCassetteManager != null && gameSceneUI != null) gameSceneUI.MakeActionCard(false, actionCassetteManager.actionCardInfos[actionKind].actionName, actionCassetteManager.actionCardInfos[actionKind].actionIcon);
                
                S_SEManager._instance.Play("s_getActionMinusCassette");
            }
        }
    }

    public void Initialize()
    {
        _onTimer = false;
        _isEnable = true;
        actionCassetteView.EnableView(true);
    }
}
