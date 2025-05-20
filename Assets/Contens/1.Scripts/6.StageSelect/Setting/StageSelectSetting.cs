using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectSetting : MonoBehaviour
{
    public Action<StageSelectSceneStatus> ChangeStatus;

    public void CursorUp()
    {
        S_SettingInfo._instance.CursorUp();
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorDown()
    {
        S_SettingInfo._instance.CursorDown();
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorRight()
    {
        S_SettingInfo._instance.CursorRight();
        S_SEManager._instance.Play("u_select");
    }   
    public void CursorLeft()
    {
        S_SettingInfo._instance.CursorLeft();
        S_SEManager._instance.Play("u_select");
    }
    public void CursorSelect()
    {
        if ( S_SettingInfo._instance.CursorSelect() )
        {
            ChangeStatus(StageSelectSceneStatus.option);
            S_SEManager._instance.Play("u_back");
            return;
        }
        S_SEManager._instance.Play("u_select");
    }
    public void CursorCancel()
    {
        S_SettingInfo._instance.CursorCancel();
        ChangeStatus(StageSelectSceneStatus.option);
        S_SEManager._instance.Play("u_back");
    }
    public void Option()
    {
        S_SettingInfo._instance.CursorCancel();
        ChangeStatus(StageSelectSceneStatus.option);
        S_SEManager._instance.Play("u_back");
    }
}
