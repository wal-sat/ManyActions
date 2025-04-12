using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastAreaHeartBeatPlayer : MonoBehaviour
{
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;

    private void Awake()
    {
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
        stageObjectCollisionArea.triggerExit = TriggerExit;
    }

    private void TriggerEnter()
    {
        S_BGMManager._instance.Pause("stage", 2f);
        S_AmbientSoundManager._instance.Play("heartBeat", 2f);
    }
    private void TriggerExit()
    {
        if (S_BGMManager._instance != null) S_BGMManager._instance.UnPause("stage", 2f);
        if (S_AmbientSoundManager._instance != null) S_AmbientSoundManager._instance.Stop("heartBeat", 2f);
    }
}
