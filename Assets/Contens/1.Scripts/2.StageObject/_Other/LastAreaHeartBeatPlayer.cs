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
        S_BGMManager._instance.Pause("stage", 0.5f);
        S_AmbientSoundManager._instance.Play("heartBeat", 0.5f);
    }
    private void TriggerExit()
    {
        S_BGMManager._instance.UnPause("stage", 0.5f);
        S_AmbientSoundManager._instance.Stop("heartBeat", 0.5f);
    }
}
