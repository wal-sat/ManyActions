using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneClearInput : MonoBehaviour
{
    [SerializeField] GameSceneClearUIToolkit gameSceneClearUIToolkit;

    private int _cursorIndex;
    int cursorIndex
    {
        get => _cursorIndex;
        set
        {
            _cursorIndex = Mathf.Clamp(value, 0, 1);

            gameSceneClearUIToolkit.OptionLabelSelect(_cursorIndex);
        }
    }

    private bool _selectPast;
    private bool _leftPast;
    private bool _rightPast;

    private void Start()
    {
        cursorIndex = 0;
    }

    public void ClearInputUpdate()
    {
        if (S_InputSystem._instance.isPushingSelect && !_selectPast) CursorSelect();
        else if (!S_InputSystem._instance.isPushingSelect && _selectPast) _selectPast = false;

        if (S_InputSystem._instance.move == Vector2.left && !_leftPast) CursorLeft();
        else if (S_InputSystem._instance.move != Vector2.left && _leftPast) _leftPast = false;

        if (S_InputSystem._instance.move == Vector2.right && !_rightPast) CursorRight();
        else if (S_InputSystem._instance.move != Vector2.right && _rightPast) _rightPast = false;
    }

    public void CursorSelect()
    {
        _selectPast = true;
        switch (cursorIndex)
        {
            case 0:
                S_LoadSceneSystem._instance.LoadScene(SceneKind.stageSelect);
            break;
            case 1:
                S_LoadSceneSystem._instance.LoadScene(S_LoadSceneSystem._instance.GetCurrentSceneKind());
            break;
        }
        S_SEManager._instance.Play("u_select");
    }
    public void CursorLeft()
    {
        cursorIndex --;
        _leftPast = true;
        S_SEManager._instance.Play("u_cursor");
    }
    public void CursorRight()
    {
        cursorIndex ++;
        _rightPast = true;
        S_SEManager._instance.Play("u_cursor");
    }
}
