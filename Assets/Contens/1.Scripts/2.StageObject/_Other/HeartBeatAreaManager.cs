using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatAreaManager : MonoBehaviour
{
    [SerializeField] SavePointManager savePointManager;
    private List<HeartBeatArea> heartBeatAreas = new List<HeartBeatArea>();

    public void Register(HeartBeatArea heartBeatArea)
    {
        heartBeatAreas.Add(heartBeatArea);
    }

    public void Lock()
    {
        foreach (var heartBeatArea in heartBeatAreas)
        {
            heartBeatArea.isLockHeartBeatArea = true;
        }
    }

    public void Initialize()
    {
        foreach (var heartBeatArea in heartBeatAreas)
        {
            heartBeatArea.isLockHeartBeatArea = false;
            if (heartBeatArea.savePoint == null || savePointManager.savePoint != heartBeatArea.savePoint) heartBeatArea.Initialize();
        }
    }
}
