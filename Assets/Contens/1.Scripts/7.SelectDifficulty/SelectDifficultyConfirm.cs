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
    }   
    public void CursorRight()
    {
        confirmIndex ++;
    }
    public void CursorSelect(DifficultyLevel level)
    {
        switch (confirmIndex)
        {
            case 0:
                CursorCancel();
            break;
            case 1:
                LoadGameScene(level);
            break;
        }
    }
    public void CursorCancel()
    {
        confirmIndex = 0;
        selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(false);
        ChangeStatus(SelectDifficultySceneStatus.menu);
    }

    private void LoadGameScene(DifficultyLevel level)
    {
        //ゲームシーンをロード
        Debug.Log(level);
    }
}
