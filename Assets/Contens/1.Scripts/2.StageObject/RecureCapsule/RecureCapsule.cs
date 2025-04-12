using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecureCapsule : MonoBehaviour
{
    enum RecureCapsuleKind { recure, spending }

    [SerializeField] RecureCapsuleManager recureCapsuleManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] RecureCapsuleView recureCapsuleView;
    [SerializeField] PlayerActionJumpManager playerActionJumpManager;
    [SerializeField] PlayerActionBlinkManager playerActionBlinkManager;
    [SerializeField] PlayerActionWarpManager playerActionWarpManager;

    [SerializeField] float COOL_TIME;
    [SerializeField] RecureCapsuleKind recureCapsuleKind;

    private float _timer;
    private bool _onTimer;
    private bool _canRecure;

    private void Awake()
    {
        _canRecure = true;

        recureCapsuleManager.Register(this);
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
                recureCapsuleView.EnableView(true);
            }
        }
    }

    private void TriggerEnter()
    {
        if (_canRecure)
        {
            _timer = 0;
            _onTimer = true;
            _canRecure = false;

            recureCapsuleView.EnableView(false);

            if (recureCapsuleKind == RecureCapsuleKind.recure)
            {
                playerActionJumpManager.Recure();
                playerActionBlinkManager.Recure();
                playerActionWarpManager.Recure();

                S_SEManager._instance.Play("s_recureCapsule");
            }
            else if (recureCapsuleKind == RecureCapsuleKind.spending)
            {
                playerActionJumpManager.Spending();
                playerActionBlinkManager.Spending();
                playerActionWarpManager.Spending();

                S_SEManager._instance.Play("s_recureCapsuleMinus");
            }
        }
    }

    public void Initialize()
    {
        _onTimer = false;
        _canRecure = true;
        recureCapsuleView.EnableView(true);
    }
}
