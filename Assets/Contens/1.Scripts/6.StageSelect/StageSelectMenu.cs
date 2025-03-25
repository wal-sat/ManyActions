using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectMenu : MonoBehaviour
{
    [SerializeField] StageSelectMenuRestrictions stageSelectMenuRestrictions;
    [SerializeField] StageSelectUIToolkit stageSelectUIToolkit;
    public Action<StageSelectSceneStatus> ChangeStatus;

    private SceneKind[,,,] _toSceneKindFromIndex = new SceneKind[5, 5, 2, 2];

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 4);

            stageSelectUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }
    private int _stageIndex;
    int stageIndex
    {
        get => _stageIndex;
        set
        {
            stageSelectUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _stageIndex = Mathf.Clamp(value, 0, 4);

            stageSelectUIToolkit.StagePanelMove(stageIndex, undergroundIndex);
            stageSelectUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            stageSelectMenuRestrictions.CheckToUndergroundStageIconDisplay(stageIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }
    private int _undergroundIndex;
    int undergroundIndex
    {
        get => _undergroundIndex;
        set
        {
            stageSelectUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _undergroundIndex = Mathf.Clamp(value, 0, 1);

            stageSelectUIToolkit.StagePanelMove(stageIndex, undergroundIndex);
            stageSelectUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }
    private int _reverseIndex;
    int reverseIndex
    {
        get => _reverseIndex;
        set
        {
            stageSelectUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _reverseIndex = Mathf.Clamp(value, 0, 1);

            stageSelectUIToolkit.StagePanelReverse(reverseIndex);
            stageSelectUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }

    private void Start()
    {
        cursorIndex = 0;
        stageIndex = 0;
        undergroundIndex = 0;
        reverseIndex = 0;

        stageSelectUIToolkit.StagePanelVisibilitySwitch(reverseIndex);

        foreach (var item in S_StageInfo._instance.stageDatas.Values)
        {
            _toSceneKindFromIndex[item.cursorIndex, item.stageIndex, item.undergroundIndex, item.reverseIndex] = item.sceneKind;
        }

        PadlockDisplayCheck();

        stageSelectMenuRestrictions.Initialize(stageIndex);
    }

    public void CursorSelect()
    {
        if (!S_StageInfo._instance.stageDatas[_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex]].isReleased) return;

        S_StageInfo._instance.SetClearStatus(_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex], true);
        for (int i = 0; i < 5; i++)
        {
            S_StageInfo._instance.SetGearAcquireStatus(_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex], i, true);
        }
        S_StageInfo._instance.SetMinimumDeathCount(_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex], 54034);
        S_StageInfo._instance.SetFastestClearTime(_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex], 46401);
        PadlockDisplayCheck();
    }
    public void CursorCancel()
    {
        
    }
    public void CursorUp()
    {
        if (undergroundIndex == 0) cursorIndex++;
        else cursorIndex--;
    }
    public void CursorDown()
    {
        if (undergroundIndex == 0) cursorIndex--;
        else cursorIndex++;
    }
    public void L()
    {
        if (!stageSelectMenuRestrictions.CanLeftArrowMove(stageIndex, undergroundIndex, reverseIndex)) return;
        cursorIndex = 0;
        stageIndex --;
    }
    public void R()
    {
        if (!stageSelectMenuRestrictions.CanRightArrowMove(stageIndex, undergroundIndex, reverseIndex)) return;
        cursorIndex = 0;
        stageIndex ++;
    }
    public void West()
    {
        if (!stageSelectMenuRestrictions.CanToUndergroundStage(stageIndex)) return;
        cursorIndex = 0;
        if (undergroundIndex == 0) undergroundIndex = 1;
        else undergroundIndex = 0;
    }
    public void North()
    {
        if (!stageSelectMenuRestrictions.CanToReverseStage()) return;
        cursorIndex = 0;
        if (reverseIndex == 0) reverseIndex = 1;
        else reverseIndex = 0;
    }
    public void Option()
    {
        
    }

    private void DisplayStageInfomation(StageData stageData)
    {
        stageSelectUIToolkit.AcquireActionImageChange(stageIndex, undergroundIndex, reverseIndex, stageData.acquireActionImage);
        stageSelectUIToolkit.StageNameLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.worldName, stageData.stageName);
        stageSelectUIToolkit.StageImageChange(stageIndex, undergroundIndex, reverseIndex, stageData.stageImage);
        for (int i = 0; i < 5; i++)
        {
            stageSelectUIToolkit.GearIconAcquired(i, stageIndex, undergroundIndex, reverseIndex, stageData.gearAcquire[i]);
        }
        stageSelectUIToolkit.MinimumDeathCountLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.minimumDeathCount);
        stageSelectUIToolkit.FastestClearTimeLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.fastestClearTime);
    }

    private void PadlockDisplayCheck()
    {
        foreach (var item in S_StageInfo._instance.stageDatas.Values)
        {
            stageSelectUIToolkit.PadlockVisibility(item.cursorIndex, item.stageIndex, item.undergroundIndex, item.reverseIndex, !item.isReleased);
        }
    }
}
