using System;
using UnityEngine;

public class PlayerActionBlinkManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionBlinkBase[] blinkActions;

    public Func<bool> IsCoolTime;

    private int _blinkTimes;
    private int _maxBlinkTimes;

    private void Awake()
    {
        foreach (var action in blinkActions)
        {
            action.init = Init;
            action.isBlinking = IsBlinking;
        }
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (IsCoolTime()) return;
        if (_blinkTimes == 0) return;
        if (_blinkTimes != -1) _blinkTimes --;

        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Blink();
        }
    }
    
    private bool IsBlinking(PlayerActionBlinkBase selfAction)
    {
        foreach (var action in blinkActions)
        {
            if (action == selfAction) continue;
            if (action.isAction) return true;
        }
        return false;
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
                case ActionKind.E_StopHover:
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
                case ActionKind.E_StopHover:
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
