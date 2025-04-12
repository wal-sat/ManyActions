using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectOption : MonoBehaviour
{
    [SerializeField] public StageSelectOptionUIToolkit stageSelectOptionUIToolkit;
    public Action<StageSelectSceneStatus> ChangeStatus;

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 2);

            stageSelectOptionUIToolkit.OptionLabelSelect(cursorIndex);
        }
    }

    private void Start()
    {
        cursorIndex = 0;
    }

    public void CursorSelect()
    {
        switch (cursorIndex)
        {
            case 0:
                OptionClose();
                S_SEManager._instance.Play("u_back");
                break;
            case 1:
                ChangeStatus(StageSelectSceneStatus.setting);
                S_SEManager._instance.Play("u_select");
                break;
            case 2:
                ChangeStatus(StageSelectSceneStatus.optionConfirm);
                S_SEManager._instance.Play("u_select");
                break;
        }
    }
    public void CursorCancel()
    {
        OptionClose();
        S_SEManager._instance.Play("u_back");
    }
    public void CursorUp()
    {
        cursorIndex--;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorDown()
    {
        cursorIndex++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void Option()
    {
        OptionClose();
        S_SEManager._instance.Play("u_back");
    }

    private void OptionClose()
    {
        ChangeStatus(StageSelectSceneStatus.menu);
    }
}
