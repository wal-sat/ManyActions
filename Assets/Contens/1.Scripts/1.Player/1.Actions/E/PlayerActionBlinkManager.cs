using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionBlinkManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionBlinkBase[] blinkActions;

    private int _blinkTimes;
    private int _maxBlinkTimes;

    private void Start()
    {
        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            action.init = Init;
        }
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (_blinkTimes == 0) return;
        if (_blinkTimes != -1) _blinkTimes --;

        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Blink();
        }
    }

    public void Recure()
    {
        _maxBlinkTimes = SetMaxBlinkTimes();
        _blinkTimes = _maxBlinkTimes;
    }
    private int SetMaxBlinkTimes()
    {
        int maxBlinkTimes = 0;
        foreach (var action in blinkActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.E_Blink:
                case ActionKind.E_UpBlink:
                case ActionKind.E_BackBlink:
                    if (action.isEnable && maxBlinkTimes == 0) maxBlinkTimes = 1;
                    break;
                case ActionKind.E_DoubleBlink:
                    if (action.isEnable && ( maxBlinkTimes == 0 || maxBlinkTimes == 1 ) ) maxBlinkTimes = 2;
                    break;
                case ActionKind.E_InfiniteBlink:
                    if (action.isEnable) maxBlinkTimes = -1;
                    break;
            }
        }
        return maxBlinkTimes;
    }

    public void ChangeBlinkTimes()
    {
        int maxBlinkTimes = 0;
        foreach (var action in blinkActions)
        {
            switch (action.actionKind)
            {
                case ActionKind.E_Blink:
                case ActionKind.E_UpBlink:
                case ActionKind.E_BackBlink:
                    if (action.isEnable && maxBlinkTimes == 0) maxBlinkTimes = 1;
                    break;
                case ActionKind.E_DoubleBlink:
                    if (action.isEnable && ( maxBlinkTimes == 0 || maxBlinkTimes == 1 ) ) maxBlinkTimes = 2;
                    break;
                case ActionKind.E_InfiniteBlink:
                    if (action.isEnable) maxBlinkTimes = -1;
                    break;
            }
        }
        
        if (maxBlinkTimes == -1) _blinkTimes = -1;
        else _blinkTimes -= _maxBlinkTimes - maxBlinkTimes;
    }
}
