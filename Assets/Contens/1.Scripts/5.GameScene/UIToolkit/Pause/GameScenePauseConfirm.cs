using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenePauseConfirm : MonoBehaviour
{
    [SerializeField] GameScenePauseUIToolkit gameScenePauseUIToolkit;

    public Action<GameSceneMenuStatus> ChangeGameSceneMenuStatus;

    private int _confirmIndex;
    int confirmIndex
    {
        get => _confirmIndex;
        set
        {
            _confirmIndex = Mathf.Clamp(value, 0, 1);

            gameScenePauseUIToolkit.ConfirmOptionsSelect(_confirmIndex);
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
    public void CursorSelect()
    {
        switch (confirmIndex)
        {
            case 0:
                CursorCancel();
            break;
            case 1:
                LoadTitleScene();
            break;
        }
    }
    public void CursorCancel()
    {
        confirmIndex = 0;
        gameScenePauseUIToolkit.ConfirmOptionsUnSelected();
        gameScenePauseUIToolkit.MenuOptionsSelect(4);
        gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(false);
        ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseMenu);
        S_SEManager._instance.Play("u_back");
    }

    private void LoadTitleScene()
    {
        S_BGMManager._instance.Stop("stage", 2f);
        S_SEManager._instance.Play("u_select");
        S_LoadSceneSystem._instance.LoadScene(SceneName.title);
    }
}
