using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMenu : MonoBehaviour
{
    [SerializeField] TitleSceneUIToolkit titleSceneUIToolkit;

    public Action<TitleSceneStatus> ChangeStatus;

    private int _menuIndex;
    int menuIndex
    {
        get => _menuIndex;
        set
        {
            //つづきからを消すため
            if (value == 1) {
                if (_menuIndex == 0) value = 2;
                if (_menuIndex == 2) value = 0;
            }

            _menuIndex = Mathf.Clamp(value, 0, 3);

            titleSceneUIToolkit.MenuOptionsSelect(_menuIndex);
        }
    }

    private void Start()
    {   
        Time.timeScale = 1;
        menuIndex = 0;

        S_BGMManager._instance.Play("title",1f);
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
                //はじめから
                S_LoadSceneSystem._instance.LoadScene(SceneKind.selectDifficulty);
            break;
            case 1:
                //つづきから
            break;
            case 2:
                ChangeStatus(TitleSceneStatus.setting);
                titleSceneUIToolkit.MenuOptionsUnSelected();
                S_SettingInfo._instance.OpenOrCloseSettingPanel(true);
            break;
            case 3:
                ChangeStatus(TitleSceneStatus.exit);
                titleSceneUIToolkit.ExitOptionsSelect(0);
                titleSceneUIToolkit.MenuOptionsUnSelected();
                titleSceneUIToolkit.OpenOrCloseExitPanel(true);
            break;
        }

        S_SEManager._instance.Play("u_select");
    }
}
