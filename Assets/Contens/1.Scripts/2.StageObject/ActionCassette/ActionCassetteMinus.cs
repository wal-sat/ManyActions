using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCassetteMinus : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
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

            actionCassetteView.OnRecure(COOL_TIME);

            playerManager.PlayerActionManager.EnableActions(stageActionData);

            playerActionJumpManager.Recure();
            playerActionBlinkManager.Recure();
            playerActionWarpManager.Recure();

            gameSceneUI.MakeActionCard(false, actionName, actionIcon);
        }
    }
}
