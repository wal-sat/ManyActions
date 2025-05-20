using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class S_StageInfo : Singleton<S_StageInfo>
{
    [Serializable] class ClearKindInfo
    {
        [SerializeField] public ClearKind clearKind;
        [SerializeField] public Sprite clearIcon;
        [SerializeField] public string clearMessage;
    }

    [SerializeField] ClearKindInfo[] clearKindInfos;

    [SerializeField] StageData[] SD_Plain_A;
    [SerializeField] StageData[] SD_Blue_F_A;
    [SerializeField] StageData[] SD_Blue_B_A;
    [SerializeField] StageData[] SD_Green_F_A;
    [SerializeField] StageData[] SD_Green_B_A;
    [SerializeField] StageData[] SD_Yellow_F_A;
    [SerializeField] StageData[] SD_Yellow_B_A;
    [SerializeField] StageData[] SD_Purple_F_A;
    [SerializeField] StageData[] SD_Purple_B_A;
    [SerializeField] StageData[] SD_Red_A;

    [SerializeField] StageData[] SD_Plain_B;
    [SerializeField] StageData[] SD_Blue_F_B;
    [SerializeField] StageData[] SD_Blue_B_B;
    [SerializeField] StageData[] SD_Green_F_B;
    [SerializeField] StageData[] SD_Green_B_B;
    [SerializeField] StageData[] SD_Yellow_F_B;
    [SerializeField] StageData[] SD_Yellow_B_B;
    [SerializeField] StageData[] SD_Purple_F_B;
    [SerializeField] StageData[] SD_Purple_B_B;
    [SerializeField] StageData[] SD_Red_B;
    [SerializeField] StageData SD_develop;

    public Dictionary<SceneKind, StageData> stageDatas = new Dictionary<SceneKind, StageData>();

    public override void Awake()
    {
        base.Awake();

        for (int i = 0; i < 5; i++)
        {
            stageDatas.Add(SD_Plain_A[i].sceneKind, SD_Plain_A[i]);
            stageDatas.Add(SD_Blue_F_A[i].sceneKind, SD_Blue_F_A[i]);
            stageDatas.Add(SD_Blue_B_A[i].sceneKind, SD_Blue_B_A[i]);
            stageDatas.Add(SD_Green_F_A[i].sceneKind, SD_Green_F_A[i]);
            stageDatas.Add(SD_Green_B_A[i].sceneKind, SD_Green_B_A[i]);
            stageDatas.Add(SD_Yellow_F_A[i].sceneKind, SD_Yellow_F_A[i]);
            stageDatas.Add(SD_Yellow_B_A[i].sceneKind, SD_Yellow_B_A[i]);
            stageDatas.Add(SD_Purple_F_A[i].sceneKind, SD_Purple_F_A[i]);
            stageDatas.Add(SD_Purple_B_A[i].sceneKind, SD_Purple_B_A[i]);
            stageDatas.Add(SD_Red_A[i].sceneKind, SD_Red_A[i]);

            stageDatas.Add(SD_Plain_B[i].sceneKind, SD_Plain_B[i]);
            stageDatas.Add(SD_Blue_F_B[i].sceneKind, SD_Blue_F_B[i]);
            stageDatas.Add(SD_Blue_B_B[i].sceneKind, SD_Blue_B_B[i]);
            stageDatas.Add(SD_Green_F_B[i].sceneKind, SD_Green_F_B[i]);
            stageDatas.Add(SD_Green_B_B[i].sceneKind, SD_Green_B_B[i]);
            stageDatas.Add(SD_Yellow_F_B[i].sceneKind, SD_Yellow_F_B[i]);
            stageDatas.Add(SD_Yellow_B_B[i].sceneKind, SD_Yellow_B_B[i]);
            stageDatas.Add(SD_Purple_F_B[i].sceneKind, SD_Purple_F_B[i]);
            stageDatas.Add(SD_Purple_B_B[i].sceneKind, SD_Purple_B_B[i]);
            stageDatas.Add(SD_Red_B[i].sceneKind, SD_Red_B[i]);

        }
        if (SD_develop != null) stageDatas.Add(SD_develop.sceneKind, SD_develop);
    }

    public void SetClearStatus(SceneKind sceneKind, bool isClear)
    {
        stageDatas[sceneKind].isClear = isClear;
    }
    public void SetGearAcquireStatus(SceneKind sceneKind, int gearIndex, bool isAcquire)
    {
        stageDatas[sceneKind].gearAcquire[gearIndex] = isAcquire;
    }
    public void AddDeathCount(SceneKind sceneKind, int deathCount, bool isClear)
    {
        stageDatas[sceneKind].SetDeathCount(deathCount, isClear);
    }
    public void AddPlayTime(SceneKind sceneKind, int playTime, bool isClear)
    {
        stageDatas[sceneKind].SetPlayTime(playTime, isClear);
    }

    public Sprite GetClearIcon(ClearKind clearKind)
    {
        foreach (var info in clearKindInfos)
        {
            if (info.clearKind == clearKind) return info.clearIcon;
        }
        return null;
    }
    public string GetClearMessage(ClearKind clearKind)
    {
        foreach (var info in clearKindInfos)
        {
            if (info.clearKind == clearKind) return info.clearMessage;
        }
        return null;
    }

    [Button]
    public void ALLClear()
    {
        foreach (var item in stageDatas.Values)
        {
            item.isClear = true;
        }
    }
}
