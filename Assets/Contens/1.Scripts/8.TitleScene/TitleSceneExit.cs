using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneExit : MonoBehaviour
{
    [SerializeField] TitleSceneUIToolkit titleSceneUIToolkit;

    public Action<TitleSceneStatus> ChangeStatus;

    private int _exitIndex;
    int exitIndex
    {
        get => _exitIndex;
        set
        {
            _exitIndex = Mathf.Clamp(value, 0, 1);

            titleSceneUIToolkit.ExitOptionsSelect(_exitIndex);
        }
    }

    private void Start()
    {   
        exitIndex = 0;
    }

    public void CursorLeft()
    {
        exitIndex --;
        S_SEManager._instance.Play("u_cursor");
    }   
    public void CursorRight()
    {
        exitIndex ++;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorSelect()
    {
        switch (exitIndex)
        {
            case 0:
                ChangeStatus(TitleSceneStatus.menu);
                titleSceneUIToolkit.MenuOptionsSelect(3);
                titleSceneUIToolkit.OpenOrCloseExitPanel(false);
                S_SEManager._instance.Play("u_back");
            break;
            case 1:
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            break;
        }
    }
}
