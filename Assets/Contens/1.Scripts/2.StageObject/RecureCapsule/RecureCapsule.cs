using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recure : MonoBehaviour
{
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] RecureCapsuleView recureCapsuleView;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] PlayerActionBlinkManager playerActionBlinkManager;
    [SerializeField] PlayerActionWarpManager playerActionWarpManager;

    [SerializeField] float COOL_TIME;

    private float _timer;
    private bool _onTimer;
    private bool _canRecure;

    private void Awake()
    {
        _canRecure = true;

        stageObjectCollisionArea.triggerEnter = TriggerEnter;
    }
    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_TIME)
            {
                _onTimer = false;
                _canRecure = true;
            }
        }
    }

    private void TriggerEnter()
    {
        if (_canRecure)
        {
            _onTimer = true;
            _canRecure = false;

            recureCapsuleView.OnRecure(COOL_TIME);

            playerActionJumpManager.Recure();
            playerActionBlinkManager.Recure();
            playerActionWarpManager.Recure();
        }
    }
}
