using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionBlinkManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] PlayerActionBlinkBase[] blinkActions;
    [HideInInspector] public int maxBlinkTimes;

    private int _blinkTimes;
    int blinkTimes
    {
        get => _blinkTimes;
        set
        {
            _blinkTimes = value;
            if (gameSceneUI != null) gameSceneUI.ChangeActionCount(ActionKind.E_Blink, value);
        }
    }

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
        if (playerMovement.IsLanding() && playerMovement.rb.velocity.y <= 5f) blinkTimes = maxBlinkTimes;
    }

    private void Init(InputKind inputKind, ActionKind actionKind)
    {
        if (blinkTimes <= 0) return;
        blinkTimes --;

        foreach (var action in blinkActions)
        {
            if (action == null) continue;

            if (action.actionKind == actionKind && action.assignedInput == inputKind) action.Blink();
        }
    }

    public void Recure()
    {
        blinkTimes = maxBlinkTimes;
    }

    public void ChangeMaxTimes(int times)
    {
        if (times < maxBlinkTimes) blinkTimes -= maxBlinkTimes - times;

        maxBlinkTimes = times;

        if (gameSceneUI == null) return;
        if (times == 0) gameSceneUI.VisibleActionCount(ActionKind.E_Blink, false);
        else gameSceneUI.VisibleActionCount(ActionKind.E_Blink, true);
    }
}
