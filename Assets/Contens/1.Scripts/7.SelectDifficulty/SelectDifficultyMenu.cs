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
    }   
    public void CursorDown()
    {
        menuY ++;
    }
    public void CursorLeft()
    {
        if (menuY == 1) return;
        menuX --;
    }   
    public void CursorRight()
    {
        if (menuY == 1) return;
        menuX ++;
    }
    public void CursorSelect()
    {
        if (menuX == 0 && menuY == 0) 
        {
            ChangeDifficultyLevel(DifficultyLevel.normal);
            selectDifficultyUIToolkit.DisplayQuestionText("ノーマルモードであそびますか？");
            selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(true);
            ChangeStatus(SelectDifficultySceneStatus.confirm);
        }
        else if (menuX == 1 && menuY == 0)
        {
            ChangeDifficultyLevel(DifficultyLevel.extra);
            selectDifficultyUIToolkit.DisplayQuestionText("エキストラモードであそびますか？");
            selectDifficultyUIToolkit.OpenOrCloseConfirmPanel(true);
            ChangeStatus(SelectDifficultySceneStatus.confirm);
        }
        else if (menuY == 1) LoadTitleScene();
    }
    public void CursorCancel()
    {
        LoadTitleScene();
    }

    private void LoadTitleScene()
    {
        //タイトルシーンにもどる
        Debug.Log("タイトルシーンへ");
    }
}
