using System;
using UnityEngine;

public enum ClearKind { key, goldGear }
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
    [SerializeField] public ClearKind clearKind;
    [SerializeField] public Sprite acquireActionImage;
    [SerializeField] public Sprite stageImage;
    [SerializeField] public ReleaseCondition[] releaseConditions;

    [HideInInspector] public bool isClear { get; set; }
    [HideInInspector] public bool[] gearAcquire {get; set;} = new bool[5];
    [HideInInspector] public int totalDeathCount { get; private set; } = 0;
    [HideInInspector] public int minimumDeathCount { get; private set; } = -1;
    [HideInInspector] public int totalPlayTime { get; private set; } = 0;
    [HideInInspector] public int fastestClearTime { get; private set; } = -1;

    [HideInInspector] public bool isReleased
    {
        get
        {
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
            return result;
        }
    }

    private bool _isPlayed;

    public void OnEnable()
    {
        isClear = false;
        for (int i = 0; i < 5; i++) gearAcquire[i] = false;
        totalDeathCount = 0;
        minimumDeathCount = -1;
        totalPlayTime = 0;
        fastestClearTime = -1;
        _isPlayed = false;
    }

    public void SetDeathCount(int count, bool isCheckMinimum = false)
    {
        if (!_isPlayed) _isPlayed = true;
        totalDeathCount += count;
        if (isCheckMinimum) 
        {
            if (minimumDeathCount == -1) minimumDeathCount = count;
            else if (count < minimumDeathCount) minimumDeathCount = count;
        }
    }

    public void SetPlayTime(int time, bool isCheckFastest = false)
    {
        if (!_isPlayed) _isPlayed = true;
        totalPlayTime += time;
        if (isCheckFastest) 
        {
            if (fastestClearTime == -1) fastestClearTime = time;
            else if (time < fastestClearTime) fastestClearTime = time;
        }
    }

    public string GetTotalDeathCountString()
    {
        if (!_isPlayed) return "- - -";
        return totalDeathCount.ToString();
    }
    public string GetMinimumDeathCountString()
    {
        if (minimumDeathCount == -1) return "- - -";
        return minimumDeathCount.ToString();
    }
    public string GetTotalPlayTimeString()
    {
        if (!_isPlayed) return "- - -";

        int hours = totalPlayTime / 3600;
        int minutes = (totalPlayTime % 3600) / 60;
        int seconds = totalPlayTime % 60;

        return string.Format("{0}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
    public string GetFastestClearTimeString()
    {
        if (fastestClearTime == -1) return "- - -";

        int hours = fastestClearTime / 3600;
        int minutes = (fastestClearTime % 3600) / 60;
        int seconds = fastestClearTime % 60;

        return string.Format("{0}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
