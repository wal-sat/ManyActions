using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatArea : MonoBehaviour
{
    [SerializeField] HeartBeatAreaManager heartBeatAreaManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] public SavePoint savePoint;

    [HideInInspector] public bool isLockHeartBeatArea;

    private void Awake()
    {
        heartBeatAreaManager.Register(this);
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
        stageObjectCollisionArea.triggerExit = TriggerExit;

        this.GetComponent<SpriteRenderer>().sprite = null;
    }
    private void Start()
    {
         S_AmbientSoundManager._instance.PlayAndPause("heartBeat");
    }

    private void TriggerEnter()
    {
        S_BGMManager._instance.Pause("stage", 1.5f);
        S_AmbientSoundManager._instance.UnPause("heartBeat", 1.5f);
    }
    private void TriggerExit()
    {
        if (isLockHeartBeatArea) return;

        if (S_BGMManager._instance != null) S_BGMManager._instance.UnPause("stage", 1.5f);
        if (S_AmbientSoundManager._instance != null) S_AmbientSoundManager._instance.Pause("heartBeat", 1.5f);
    }

    public void Initialize()
    {
        if (S_BGMManager._instance != null) S_BGMManager._instance.UnPause("stage", 1.5f);
        if (S_AmbientSoundManager._instance != null) S_AmbientSoundManager._instance.Pause("heartBeat", 1.5f);
    }
}
