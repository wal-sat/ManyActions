using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficultyConfirm : MonoBehaviour
{
    [SerializeField] SelectDifficultyUIToolkit selectDifficultyUIToolkit;

    public Action<SelectDifficultySceneStatus> ChangeStatus;

    private int _confirmIndex;
    int confirmIndex
    {
        get => _confirmIndex;
        set
        {
            _confirmIndex = Mathf.Clamp(value, 0, 1);

            selectDifficultyUIToolkit.ConfirmOptionsSelect(_confirmIndex);
        }
    }

    private void Start()
    {   
        confirmIndex = 0;
    }

    public void CursorLeft()
    {
        confirmIndex --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorRight()
    {
        confirmIndex ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorSelect(DifficultyLevel level)
    {
        switch (confirmIndex)
        {
            case 0:
                CursorCancel(level);
            break;
            case 1:
                LoadGameScene(level);

                S_BGMManager._instance.Stop("title", 2f);
                S_SEManager._instance.Play("u_select");
            break;
        }
    }
    public void CursorCancel(DifficultyLevel level)
    {
        confirmIndex = 0;
        selectDifficultyUIToolkit.ConfirmOptionsUnSelect();
        if (level == DifficultyLevel.normal) selectDifficultyUIToolkit.CardSelect(0,0);
        if (level == DifficultyLevel.extra) selectDifficultyUIToolkit.CardSelect(1,0);
        selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(false);
        ChangeStatus(SelectDifficultySceneStatus.menu);

        S_SEManager._instance.Play("u_back");
    }

    private void LoadGameScene(DifficultyLevel level)
    {
        // if (level == DifficultyLevel.normal) S_LoadSceneSystem._instance.LoadScene(SceneKind.normalStage_0);
        // if (level == DifficultyLevel.extra) S_LoadSceneSystem._instance.LoadScene(SceneKind.extraStage_0);
    }
}
