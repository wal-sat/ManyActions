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
}
