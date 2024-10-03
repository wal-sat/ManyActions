using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionRequireCoolDownBase : PlayerActionBase
{
    [SerializeField] private float COOL_DOWN_TIME;
    [HideInInspector] public bool isCoolDowning;
    private float _timer;
    private bool _onTimer;

    public virtual void Update()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_DOWN_TIME)
            {
                _onTimer = false;
                isCoolDowning = false;
            }
        }
    }

    public override void InitAction()
    {
        _timer = 0;
        _onTimer = true;

        isCoolDowning = true;
    }
}
