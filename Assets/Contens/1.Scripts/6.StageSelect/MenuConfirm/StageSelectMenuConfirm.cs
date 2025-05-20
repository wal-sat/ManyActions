using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectMenuConfirm : MonoBehaviour
{
    [SerializeField] public StageSelectMenuConfirmUIToolkit stageSelectMenuConfirmUIToolkit;
    public Action<StageSelectSceneStatus> ChangeStatus;

    private StageData _stageData;
    [HideInInspector] public StageData stageData
    {
        get => _stageData;
        set
        {
            _stageData = value;

            stageSelectMenuConfirmUIToolkit.TitleLabelChange(_stageData.worldName + _stageData.stageName);
        }
    }

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 1);

            stageSelectMenuConfirmUIToolkit.OptionLabelSelect(_cursorIndex);
        }
    }

    private void Start()
    {   
        cursorIndex = 0;
    }

    public void CursorLeft()
    {
        cursorIndex --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorRight()
    {
        cursorIndex ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorSelect()
    {
        switch (cursorIndex)
        {
            case 0:
                S_LoadSceneSystem._instance.LoadScene(stageData.sceneKind);
                S_SEManager._instance.Play("u_select");
            break;
            case 1:
                PanelClose();
            break;
        }
    }
    public void CursorCancel()
    {
        PanelClose();
    }
    public void Option()
    {
        PanelClose();
    }

    private void PanelClose()
    {
        cursorIndex = 0;
        ChangeStatus(StageSelectSceneStatus.menu);

        S_SEManager._instance.Play("u_back");
    }
}
