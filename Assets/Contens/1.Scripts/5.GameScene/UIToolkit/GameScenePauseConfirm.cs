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
    }   
    public void CursorRight()
    {
        confirmIndex ++;
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
        gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(false);
        ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseMenu);
    }

    private void LoadTitleScene()
    {
        S_LoadSceneSystem._instance.LoadScene(SceneName.title);
    }
}
