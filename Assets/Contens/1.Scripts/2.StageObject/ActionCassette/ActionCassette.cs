using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCassette : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] ActionCassetteView actionCassetteView;
    [SerializeField] StageActionData stageActionData;

    [SerializeField] float COOL_TIME;

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
            _onTimer = true;
            _isEnable = false;

            actionCassetteView.OnRecure(COOL_TIME);

           playerManager.PlayerActionManager.EnableActions(stageActionData);
        }
    }
}
