using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameInfo : Singleton<S_GameInfo>
{
    //ーーーーー歯車ーーーーー
    public int totalGearCount
    {
        get
        {
            int count = 0;
            foreach (StageData stageData in S_StageInfo._instance.stageDatas.Values)
            {
                for (int i = 0; i < stageData.gearAcquire.Length; i++)
                {
                    if (stageData.gearAcquire[i]) count++;
                }
            }
            return count;
        }
    }

    public int GetGearCountInAScene(SceneKind sceneKind)
    {
        int count = 0;
        foreach (bool gearAcquire in S_StageInfo._instance.stageDatas[sceneKind].gearAcquire)
        {
            if (gearAcquire) count++;
        }
        return count;
    }

    //ーーーーーデス数ーーーーー
    public int totalDeathCount
    {
        get
        {
            int count = 0;
            foreach (StageData stageData in S_StageInfo._instance.stageDatas.Values)
            {
                count += stageData.totalDeathCount;
            }
            return count;
        }
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
