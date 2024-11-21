using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlinkKind { Blink, BackBlink, UpBlink }

public class PlayerActionBlinkManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerActionBlinkBase[] blinkActions;
    [SerializeField] int MAX_BLINK_TIMES;

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
        if (playerMovement.IsLanding()) _blinkTimes = MAX_BLINK_TIMES;
    }

    private void Init(InputKind inputKind, BlinkKind blinkKind)
    {
        if (_blinkTimes <= 0) return;
        _blinkTimes --;

        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            if (action.blinkKind == blinkKind && action.assignedInput == inputKind) action.Blink();
        }
    }
}
