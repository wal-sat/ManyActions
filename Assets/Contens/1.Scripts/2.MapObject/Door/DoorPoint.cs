using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPoint : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] DoorPointArea doorPointArea;
    [SerializeField] SceneName sceneName;

    private void Awake()
    {
        doorPointArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        stageManager.Door(sceneName);
    }
}
