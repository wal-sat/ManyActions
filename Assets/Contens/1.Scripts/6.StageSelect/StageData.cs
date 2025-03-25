using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;

public enum ReleaseConditionKind { None, Clear, GearComplete, GearCount }

[Serializable]
public class ReleaseCondition
{
    [SerializeField] public ReleaseConditionKind releaseConditionKind;
    [Header("Clearの場合")][SerializeField] public SceneKind clearSceneKind;
    [Header("GearCompleteの場合")][SerializeField] public SceneKind gearCompleteSceneKind;
    [Header("GearCountの場合")][SerializeField] public int getGearCount;
}

[CreateAssetMenu(menuName = "ScriptableObject/StageData", fileName = "SD_")]
public class StageData : ScriptableObject
{
    [SerializeField] public SceneKind sceneKind;
    [SerializeField][Range(0, 4)] public int cursorIndex;
    [SerializeField][Range(0, 4)] public int stageIndex;
    [SerializeField][Range(0, 1)] public int undergroundIndex;
    [SerializeField][Range(0, 1)] public int reverseIndex;
    [SerializeField] public string worldName;
    [SerializeField] public string stageName;
    [SerializeField] public Sprite acquireActionImage;
    [SerializeField] public Sprite stageImage;
    [SerializeField] public AcquireActionData acquireActionData;
    [SerializeField] public ReleaseCondition[] releaseConditions;

    private bool _isReleased;
    [HideInInspector] public bool isReleased
    {
        get
        {
            if (_isReleased) return true;

            bool result = true;
            foreach (var releaseCondition in releaseConditions)
            {
                switch (releaseCondition.releaseConditionKind)
                {
                    case ReleaseConditionKind.None:
                        break;
                    case ReleaseConditionKind.Clear:
                        if (!S_StageInfo._instance.stageDatas[releaseCondition.clearSceneKind].isClear) result = false;
                        break;
                    case ReleaseConditionKind.GearComplete:
                        for (int i = 0; i < 5; i++)
                        {
                            if (!S_StageInfo._instance.stageDatas[releaseCondition.gearCompleteSceneKind].gearAcquire[i])
                            {
                                result = false;
                                break;
                            }
                        }
                        break;
                    case ReleaseConditionKind.GearCount:
                        if (S_GameInfo._instance.totalGearCount < releaseCondition.getGearCount) result = false;
                        break;
                }
            }
            _isReleased = result;
            return result;
        }
        set => _isReleased = value;
    }

    [HideInInspector] public bool isClear { get; set; }
    [HideInInspector] public bool[] gearAcquire {get; set;} = new bool[5];

    private int _minimumDeathCount;
    [HideInInspector] public int minimumDeathCount
    {
        get => _minimumDeathCount;
        set
        {
            if (_minimumDeathCount == -1) _minimumDeathCount = value;
            else if (value < _minimumDeathCount) _minimumDeathCount = value;
        }
    }
    private int _fastestClearTime;
    [HideInInspector] public int fastestClearTime
    {
        get => _fastestClearTime;
        set
        {
            if (_fastestClearTime == -1) _fastestClearTime = value;
            else if (value < _fastestClearTime) _fastestClearTime = value;
        }
    }

    private void OnEnable()
    {
        isReleased = false;
        isClear = false;
        for (int i = 0; i < 5; i++)
        {
            gearAcquire[i] = false;
        }
        minimumDeathCount = -1;
        fastestClearTime = -1;
    }
}
