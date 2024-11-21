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
            _menuIndex = Mathf.Clamp(value, 0, 3);

            titleSceneUIToolkit.MenuOptionsSelect(_menuIndex);
        }
    }

    private void Start()
    {   
        //もしセーブデータがあるなら、１にする。
        menuIndex = 0;
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
                //はじめから
                S_LoadSceneSystem._instance.LoadScene(SceneName.selectDifficulty);
            break;
            case 1:
                //つづきから
            break;
            case 2:
                ChangeStatus(TitleSceneStatus.setting);
                titleSceneUIToolkit.OpenOrCloseSettingPanel(true);
            break;
            case 3:
                ChangeStatus(TitleSceneStatus.exit);
                titleSceneUIToolkit.OpenOrCloseExitPanel(true);
            break;
        }
    }
}
