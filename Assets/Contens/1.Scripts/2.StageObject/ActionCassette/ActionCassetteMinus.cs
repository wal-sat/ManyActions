using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCassetteMinus : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] ActionCassetteManager actionCassetteManager;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] PlayerActionBlinkManager playerActionBlinkManager;
    [SerializeField] PlayerActionWarpManager playerActionWarpManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] ActionCassetteView actionCassetteView;
    [SerializeField] StageActionData stageActionData;
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] float COOL_TIME;
    [SerializeField] string actionName;
    [SerializeField] Sprite actionIcon;


    private float _timer;
    private bool _onTimer;
    private bool _isEnable;

    private void Awake()
    {
        _isEnable = true;

        actionCassetteManager.RegisterMinus(this);
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

            playerManager.PlayerActionManager.EnableActions(stageActionData);

            gameSceneUI.MakeActionCard(false, actionName, actionIcon);

            S_SEManager._instance.Play("s_getActionMinusCassette");
        }
    }
    
    public void Initialize()
    {
        _onTimer = false;
        _isEnable = true;
        actionCassetteView.EnableView(true);
    }
}
