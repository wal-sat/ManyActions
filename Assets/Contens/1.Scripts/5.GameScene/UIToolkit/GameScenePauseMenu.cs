using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScenePauseMenu : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] GameScenePauseUIToolkit gameScenePauseUIToolkit;

    public Action<GameSceneMenuStatus> ChangeGameSceneMenuStatus;

    private int _menuIndex;
    int menuIndex
    {
        get => _menuIndex;
        set
        {
            _menuIndex = Mathf.Clamp(value, 0, 2);

            gameScenePauseUIToolkit.MenuOptionsSelect(_menuIndex);
        }
    }

    private void Start()
    {
        menuIndex = 0;

        gameScenePauseUIToolkit.RootSetActive(false);
    }

    private void ClosePauseMenuPanel()
    {
        stageManager.ClosePausePanel();
    }

    public void CursorUp()
    {
        menuIndex --;
    }   
    public void CursorDown()
    {
        menuIndex ++;
    }
    public void CursorSelect()
    {
        switch (menuIndex)
        {
            case 0:
                ClosePauseMenuPanel();
            break;
            case 1:
                gameScenePauseUIToolkit.OpenOrCloseSettingPanel(true);
                ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseSetting);
            break;
            case 2:
                gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(true);
                ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseConfirm);
            break;
        }
    }
    public void CursorCancel()
    {
        ClosePauseMenuPanel();
    }
}
