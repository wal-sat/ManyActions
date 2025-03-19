using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficultyMenu : MonoBehaviour
{
    [SerializeField] SelectDifficultyUIToolkit selectDifficultyUIToolkit;

    public Action<SelectDifficultySceneStatus> ChangeStatus;
    public Action<DifficultyLevel> ChangeDifficultyLevel;

    private int _menuX;
    int menuX
    {
        get => _menuX;
        set
        {
            _menuX = Mathf.Clamp(value, 0, 1);

            selectDifficultyUIToolkit.CardSelect(_menuX, _menuY);
        }
    }
    private int _menuY;
    int menuY
    {
        get => _menuY;
        set
        {
            _menuY = Mathf.Clamp(value, 0, 1);

            selectDifficultyUIToolkit.CardSelect(_menuX, _menuY);
        }
    }

    private void Start()
    {   
        menuX = 0;
        menuY = 0;
    }

    public void CursorUp()
    {
        menuY --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorDown()
    {
        menuY ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorLeft()
    {
        if (menuY == 1) return;
        menuX --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorRight()
    {
        if (menuY == 1) return;
        menuX ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorSelect()
    {
        if (menuX == 0 && menuY == 0) 
        {
            ChangeDifficultyLevel(DifficultyLevel.normal);
            selectDifficultyUIToolkit.CardUnSelect();
            selectDifficultyUIToolkit.ConfirmOptionsSelect(0);
            selectDifficultyUIToolkit.DisplayQuestionText("ノーマルモードであそびますか？");
            selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(true);
            ChangeStatus(SelectDifficultySceneStatus.confirm);

            S_SEManager._instance.Play("u_select");
        }
        else if (menuX == 1 && menuY == 0)
        {
            ChangeDifficultyLevel(DifficultyLevel.extra);
            selectDifficultyUIToolkit.CardUnSelect();
            selectDifficultyUIToolkit.ConfirmOptionsSelect(0);
            selectDifficultyUIToolkit.DisplayQuestionText("エキストラモードであそびますか？");
            selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(true);
            ChangeStatus(SelectDifficultySceneStatus.confirm);

            S_SEManager._instance.Play("u_select");
        }
        else if (menuY == 1)
        {
            LoadTitleScene();

            S_SEManager._instance.Play("u_back");
        }
    }
    public void CursorCancel()
    {
        LoadTitleScene();

        S_SEManager._instance.Play("u_back");
    }

    private void LoadTitleScene()
    {
        S_LoadSceneSystem._instance.LoadScene(SceneKind.title);
    }
}
