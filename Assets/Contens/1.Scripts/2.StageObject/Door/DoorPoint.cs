using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPoint : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] SceneName sceneName;

    private void Awake()
    {
        stageObjectCollisionArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        stageManager.Door(sceneName);
    }
}
