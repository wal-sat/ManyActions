using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageSelectMenu : MonoBehaviour
{
    [SerializeField] StageSelectMenuRestrictions stageSelectMenuRestrictions;
    [SerializeField] public StageSelectMenuConfirm stageSelectMenuConfirm;
    [SerializeField] public StageSelectMenuUIToolkit stageSelectMenuUIToolkit;
    [SerializeField] StageSelectCameraMovement stageSelectCameraMovement;

    public Action<StageSelectSceneStatus> ChangeStatus;

    private SceneKind[,,,] _toSceneKindFromIndex = new SceneKind[5, 5, 2, 2];

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 4);

            stageSelectMenuUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }
    private int _stageIndex;
    int stageIndex
    {
        get => _stageIndex;
        set
        {
            stageSelectMenuUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _stageIndex = Mathf.Clamp(value, 0, 4);

            stageSelectCameraMovement.SetCameraPosition(stageIndex, undergroundIndex);
            stageSelectMenuUIToolkit.StagePanelMove(stageIndex, undergroundIndex);
            stageSelectMenuUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
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
            stageSelectMenuUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _undergroundIndex = Mathf.Clamp(value, 0, 1);

            stageSelectCameraMovement.SetCameraPosition(stageIndex, undergroundIndex);
            stageSelectMenuUIToolkit.StagePanelMove(stageIndex, undergroundIndex);
            stageSelectMenuUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }
    private int _reverseIndex;
    int reverseIndex
    {
        get => _reverseIndex;
        set
        {
            stageSelectMenuUIToolkit.StageLabelUnSelect(stageIndex, undergroundIndex, reverseIndex);

            _reverseIndex = Mathf.Clamp(value, 0, 1);

            stageSelectMenuUIToolkit.StagePanelReverse(reverseIndex, stageSelectCameraMovement.SetEnviroment);
            stageSelectMenuUIToolkit.StageLabelSelect(cursorIndex, stageIndex, undergroundIndex, reverseIndex);
            DisplayStageInfomation(S_StageInfo._instance.stageDatas[ _toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex] ]);
        }
    }

    private void Start()
    {
        cursorIndex = 0;
        stageIndex = 0;
        undergroundIndex = 0;
        reverseIndex = 0;

        stageSelectMenuUIToolkit.StagePanelVisibilitySwitch(reverseIndex);

        foreach (var item in S_StageInfo._instance.stageDatas.Values)
        {
            _toSceneKindFromIndex[item.cursorIndex, item.stageIndex, item.undergroundIndex, item.reverseIndex] = item.sceneKind;
        }

        PadlockDisplayCheck();

        stageSelectMenuRestrictions.Initialize(stageIndex);

        S_BGMManager._instance.Play("select", 2f);
    }

    public void CursorSelect()
    {
        if (!S_StageInfo._instance.stageDatas[_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex]].isReleased)
        {
            S_SEManager._instance.Play("u_restrict");
            return;
        }

        ChangeStatus(StageSelectSceneStatus.menuConfirm);
        stageSelectMenuConfirm.stageData = S_StageInfo._instance.stageDatas[_toSceneKindFromIndex[cursorIndex, stageIndex, undergroundIndex, reverseIndex]];
        S_SEManager._instance.Play("u_select");
    }
    public void CursorCancel()
    {
        
    }
    public void CursorUp()
    {
        if (undergroundIndex == 0) cursorIndex++;
        else cursorIndex--;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorDown()
    {
        if (undergroundIndex == 0) cursorIndex--;
        else cursorIndex++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void L()
    {
        if (!stageSelectMenuRestrictions.CanLeftArrowMove(stageIndex, undergroundIndex, reverseIndex))
        {
            S_SEManager._instance.Play("u_restrict");
            return;
        }
        stageIndex --;
        S_SEManager._instance.Play("u_cursor");
    }
    public void R()
    {
        if (!stageSelectMenuRestrictions.CanRightArrowMove(stageIndex, undergroundIndex, reverseIndex))
        {
            S_SEManager._instance.Play("u_restrict");
            return;
        }
        stageIndex ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void West()
    {
        if (!stageSelectMenuRestrictions.CanToUndergroundStage(stageIndex)) return;
        if (undergroundIndex == 0) undergroundIndex = 1;
        else undergroundIndex = 0;
        S_SEManager._instance.Play("u_select");
    }
    public void North()
    {
        if (!stageSelectMenuRestrictions.CanToReverseStage()) return;
        if (reverseIndex == 0) reverseIndex = 1;
        else reverseIndex = 0;
        S_SEManager._instance.Play("u_select");
    }
    public void Option()
    {
        ChangeStatus(StageSelectSceneStatus.option);
        S_SEManager._instance.Play("u_pause");
    }

    private void DisplayStageInfomation(StageData stageData)
    {
        stageSelectMenuUIToolkit.AcquireActionImageChange(stageIndex, undergroundIndex, reverseIndex, stageData.acquireActionImage);
        stageSelectMenuUIToolkit.StageNameLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.worldName, stageData.stageName);
        stageSelectMenuUIToolkit.StageImageChange(stageIndex, undergroundIndex, reverseIndex, stageData.stageImage);
        for (int i = 0; i < 5; i++)
        {
            stageSelectMenuUIToolkit.GearIconAcquired(i, stageIndex, undergroundIndex, reverseIndex, stageData.gearAcquire[i]);
        }
        stageSelectMenuUIToolkit.MinimumDeathCountLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.GetMinimumDeathCountString());
        stageSelectMenuUIToolkit.TotalDeathCountLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.GetTotalDeathCountString());
        stageSelectMenuUIToolkit.FastestClearTimeLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.GetFastestClearTimeString());
        stageSelectMenuUIToolkit.TotalPlayTimeLabelChange(stageIndex, undergroundIndex, reverseIndex, stageData.GetTotalPlayTimeString());
    }

    private void PadlockDisplayCheck()
    {
        foreach (var item in S_StageInfo._instance.stageDatas.Values)
        {
            stageSelectMenuUIToolkit.PadlockVisibility(item.cursorIndex, item.stageIndex, item.undergroundIndex, item.reverseIndex, !item.isReleased);
        }
    }
}
