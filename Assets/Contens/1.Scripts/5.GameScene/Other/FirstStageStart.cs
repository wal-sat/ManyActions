using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageStart : MonoBehaviour
{
    private void Awake()
    {
        S_GameInfo._instance.ResetDeathCount();
        S_GameInfo._instance.ResetTime();
    }
    private void Start()
    {
        S_GameInfo._instance.onTimer = true;

        S_BGMManager._instance.Play("stage", 2f);
    }
}
