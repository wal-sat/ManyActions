using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionBlinkManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionBlinkBase[] blinkActions;
    [HideInInspector] public int maxBlinkTimes;

    private int _blinkTimes;

    private void Start()
    {
        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            action.init = Init;
        }
    }

    private void Update()
    {
        if (playerMovement.IsLanding() && playerMovement.rb.velocity.y <= 5f) _blinkTimes = maxBlinkTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (_blinkTimes <= 0) return;
        _blinkTimes --;

        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Blink();
        }
    }

    public void Recure()
    {
        _blinkTimes = maxBlinkTimes;
    }

    public void ChangeMaxTimes(int times)
    {
        if (times < maxBlinkTimes) _blinkTimes -= maxBlinkTimes - times;

        maxBlinkTimes = times;
    }
}
