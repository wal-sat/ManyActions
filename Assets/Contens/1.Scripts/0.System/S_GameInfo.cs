using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameInfo : Singleton<S_GameInfo>
{
    //ーーーーー歯車ーーーーー
    private int _totalGearCount;
    public int totalGearCount
    {
        get
        {
            _totalGearCount = 0;
            foreach (StageData stageData in S_StageInfo._instance.stageDatas.Values)
            {
                for (int i = 0; i < stageData.gearAcquire.Length; i++)
                {
                    if (stageData.gearAcquire[i]) _totalGearCount++;
                }
            }
            return _totalGearCount;
        }
    }

    public int GetGearCount(SceneKind sceneKind)
    {
        int count = 0;
        foreach (bool gearAcquire in S_StageInfo._instance.stageDatas[sceneKind].gearAcquire)
        {
            if (gearAcquire) count++;
        }
        return count;
    }

    //ーーーーーデス数ーーーーー
    public int deathCount = 0;
    public int totalDeathCount = 0;
    public void DeathCountIncrement()
    {
        deathCount ++;
        totalDeathCount ++;
    }
    public void ResetDeathCount()
    {
        deathCount = 0;

    }

    //ーーーーータイムーーーーー
    public bool onTimer;
    private float time;
    private float totalTime;

    private void FixedUpdate()
    {
        if (onTimer) 
        {
            time += Time.deltaTime;
            totalTime += Time.deltaTime;
        }
    }
    public int[] GetTime()
    {
        int[] ints = new int[3];

        ints[0] = (int) time / 3600;
        ints[1] = (int) ( time % 3600 ) / 60;
        ints[2] = (int) time % 60;

        return ints;
    }
    public int[] GetTotalTime()
    {
        int[] ints = new int[3];

        ints[0] = (int) totalTime / 3600;
        ints[1] = (int) ( totalTime % 3600 ) / 60;
        ints[2] = (int) totalTime % 60;

        return ints;
    }
    public void ResetTime()
    {
        time = 0;
    }
}
