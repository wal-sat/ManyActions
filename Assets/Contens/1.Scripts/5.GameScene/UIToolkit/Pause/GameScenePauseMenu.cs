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
    public int menuIndex
    {
        get => _menuIndex;
        set
        {
            _menuIndex = Mathf.Clamp(value, 0, 3);

            gameScenePauseUIToolkit.MenuOptionsSelect(_menuIndex);
        }
    }

    private void Start()
    {
        menuIndex = 0;
        
        gameScenePauseUIToolkit.RootSetActive(false);
        gameScenePauseUIToolkit.MenuOptionsUnSelect();
    }

    public void CursorUp()
    {
        menuIndex --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorDown()
    {
        menuIndex ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorSelect()
    {
        switch (menuIndex)
        {
            case 0:
                stageManager.ClosePausePanel();
                S_SEManager._instance.Play("u_back");
            break;
            case 1:
                stageManager.ClosePausePanel();
                stageManager.Restart();
            break;
            case 2:
                gameScenePauseUIToolkit.MenuOptionsUnSelect();
                S_SettingInfo._instance.OpenOrCloseSettingPanel(true);
                ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseSetting);
                S_SEManager._instance.Play("u_select");
            break;
            case 3:
                gameScenePauseUIToolkit.MenuOptionsUnSelect();
                gameScenePauseUIToolkit.ConfirmOptionsSelect(0);
                gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(true);
                ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseConfirm);
                S_SEManager._instance.Play("u_select");
            break;
        }
    }
    public void CursorCancel()
    {
        stageManager.ClosePausePanel();
        S_SEManager._instance.Play("u_back");
    }
}
