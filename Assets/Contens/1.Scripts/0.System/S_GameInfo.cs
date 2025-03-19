using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameInfo : Singleton<S_GameInfo>
{
    //ーーーーー歯車ーーーーー
    public Dictionary<SceneKind, bool[]> gearInfo = new Dictionary<SceneKind, bool[]>();

    public void InstantiateGearInfo(SceneKind sceneKind, int gearNumber)
    {
        if (!gearInfo.ContainsKey(sceneKind))
        {
            gearInfo.Add(sceneKind, new bool[gearNumber]);
        }
        else Debug.LogWarning($"{sceneKind}のブール値配列は既に生成されています");
    }
    public void RegisterGearInfo(SceneKind sceneKind, int gearIndex, bool isAcquired)
    {
        gearInfo[sceneKind][gearIndex] = isAcquired;
    }

    public int GetGearCount()
    {
        int count = 0;
        foreach (bool[] info in gearInfo.Values)
        {
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i]) count ++;
            }
        }
        return count;
    }
    public void GearCountReset()
    {
        foreach (bool[] info in gearInfo.Values)
        {
            for (int i = 0; i < info.Length; i++)
            {
                info[i] = false;
            }
        }
    }

    //ーーーーーデス数ーーーーー
    public int deathCount = 0;
    public int DeathCountIncrement()
    {
        deathCount ++;
        return deathCount;
    }
    public int DeathCountReset()
    {
        deathCount = 0;
        return deathCount;
    }

    //ーーーーータイムーーーーー
    public bool onTimer;
    private float time;

    private void FixedUpdate()
    {
        if (onTimer) time += Time.deltaTime;
    }
    public int[] GetMiniteAndSecond()
    {
        int[] ints = new int[2];

        ints[0] = (int) time / 60;
        ints[1] = (int) time % 60;

        return ints;
    }
    public void TimeReset()
    {
        time = 0;
    }
}
