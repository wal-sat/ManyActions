using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StageSelectMenuRestrictions : MonoBehaviour
{
    [SerializeField] StageSelectMenuUIToolkit stageSelectMenuUIToolkit;

    public void Initialize(int stageIndex)
    {
        stageSelectMenuUIToolkit.LeftArrowVisibility(0, 0, 0, false);
        stageSelectMenuUIToolkit.LeftArrowVisibility(0, 1, 0, false);
        stageSelectMenuUIToolkit.LeftArrowVisibility(0, 0, 1, false);
        stageSelectMenuUIToolkit.LeftArrowVisibility(0, 1, 1, false);
        stageSelectMenuUIToolkit.RightArrowVisibility(4, 0, 0, false);
        stageSelectMenuUIToolkit.RightArrowVisibility(4, 1, 0, false);
        stageSelectMenuUIToolkit.RightArrowVisibility(4, 0, 1, false);
        stageSelectMenuUIToolkit.RightArrowVisibility(4, 1, 1, false);

        CheckArrowVisibility();
        CheckToUndergroundStageIconDisplay(stageIndex);
        CheckToReverseStageIconDisplay();
    }

    public void CheckArrowVisibility()
    {
        if (!S_StageInfo._instance.stageDatas[SceneKind.blue_F5_A].isClear) stageSelectMenuUIToolkit.RightArrowVisibility(1, 0, 0, false);
        else stageSelectMenuUIToolkit.RightArrowVisibility(1, 0, 0, true);

        if (!S_StageInfo._instance.stageDatas[SceneKind.blue_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.green_B5_A].isClear ||
            !S_StageInfo._instance.stageDatas[SceneKind.yellow_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.purple_B5_A].isClear) stageSelectMenuUIToolkit.LeftArrowVisibility(1, 1, 0, false);
        else stageSelectMenuUIToolkit.LeftArrowVisibility(1, 1, 0, true);
    }

    public void CheckToUndergroundStageIconDisplay(int stageIndex)
    {
        if (!S_StageInfo._instance.stageDatas[SceneKind.blue_F5_A].isClear)
        {
            stageSelectMenuUIToolkit.ToUndergroundStageIconDisplay(false);
        }
        else if (!S_StageInfo._instance.stageDatas[SceneKind.blue_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.green_B5_A].isClear ||
                 !S_StageInfo._instance.stageDatas[SceneKind.yellow_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.purple_B5_A].isClear)
        {
            if (stageIndex == 0) stageSelectMenuUIToolkit.ToUndergroundStageIconDisplay(false);
            else stageSelectMenuUIToolkit.ToUndergroundStageIconDisplay(true);
        }
        else stageSelectMenuUIToolkit.ToUndergroundStageIconDisplay(true);
    }

    public void CheckToReverseStageIconDisplay()
    {
        if (!S_StageInfo._instance.stageDatas[SceneKind.red_5_A].isClear) stageSelectMenuUIToolkit.ToReverseStageIconDisplay(false);
        else stageSelectMenuUIToolkit.ToReverseStageIconDisplay(true);
    }

    public bool CanLeftArrowMove(int stageIndex, int undergroundIndex, int reverseIndex)
    {
        if (stageIndex == 0) return false;
        else if (!S_StageInfo._instance.stageDatas[SceneKind.blue_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.green_B5_A].isClear ||
            !S_StageInfo._instance.stageDatas[SceneKind.yellow_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.purple_B5_A].isClear)
        {
            if (stageIndex == 1 && undergroundIndex == 1 && reverseIndex == 0) return false;
        }
        return true;
    }
    public bool CanRightArrowMove(int stageIndex, int undergroundIndex, int reverseIndex)
    {
        if (stageIndex == 4) return false;
        else if (!S_StageInfo._instance.stageDatas[SceneKind.blue_F5_A].isClear)
        {
            if (stageIndex == 1 && undergroundIndex == 0 && reverseIndex == 0) return false;
        }
        return true;
    }
    public bool CanToUndergroundStage(int stageIndex)
    {
        if (!S_StageInfo._instance.stageDatas[SceneKind.blue_F5_A].isClear)
        {
            return false;
        }
        else if (!S_StageInfo._instance.stageDatas[SceneKind.blue_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.green_B5_A].isClear ||
                 !S_StageInfo._instance.stageDatas[SceneKind.yellow_B5_A].isClear || !S_StageInfo._instance.stageDatas[SceneKind.purple_B5_A].isClear)
        {
            if (stageIndex == 0) return false;
            else return true;
        }
        else return true;
    }
    public bool CanToReverseStage()
    {
        if (!S_StageInfo._instance.stageDatas[SceneKind.red_5_A].isClear) return false;
        else return true;
    }
}
