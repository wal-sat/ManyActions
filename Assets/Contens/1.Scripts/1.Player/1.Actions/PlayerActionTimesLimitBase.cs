using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionTimesLimitBase : PlayerActionBase
{
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public int MAX_USE_TIMES;

    [HideInInspector] public bool isLimited;
    [HideInInspector] public int useTimes;

    private void Update()
    {
        if (playerMovement.IsLanding() && isLimited) Recover();
    }

    public override void EndAction()
    {
        useTimes --;
        if (useTimes <= 0) isLimited = true;
    }

    private void Recover()
    {
        isLimited = false;
        useTimes = MAX_USE_TIMES;
    }
}
