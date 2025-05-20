using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectOptionConfirm : MonoBehaviour
{
    [SerializeField] public StageSelectOptionConfirmUIToolkit  stageSelectOptionConfirmUIToolkit;
    public Action<StageSelectSceneStatus> ChangeStatus;

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 1);

            stageSelectOptionConfirmUIToolkit.OptionLabelSelect(_cursorIndex);
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
                S_LoadSceneSystem._instance.LoadScene(SceneKind.title);
                S_SEManager._instance.Play("u_select");
            break;
            case 1:
                ChangeStatus(StageSelectSceneStatus.option);
                
                S_SEManager._instance.Play("u_back");
            break;
        }
    }
    public void CursorCancel()
    {
        ChangeStatus(StageSelectSceneStatus.option);

        S_SEManager._instance.Play("u_back");
    }
    public void Option()
    {
        ChangeStatus(StageSelectSceneStatus.option);

        S_SEManager._instance.Play("u_back");
    }
}

