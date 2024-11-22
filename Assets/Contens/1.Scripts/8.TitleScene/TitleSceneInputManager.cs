using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleSceneStatus { menu, setting, exit }

public class TitleSceneInputManager : MonoBehaviour
{
    [SerializeField] TitleSceneMenu titleSceneMenu;
    [SerializeField] TitleSceneSetting titleSceneSetting;
    [SerializeField] TitleSceneExit titleSceneExit;

    private TitleSceneStatus _titleSceneStatus;

    private bool _selectPast;
    private bool _cancelPast;
    private bool _upPast;
    private bool _downPast;
    private bool _leftPast;
    private bool _rightPast;

    private void Awake()
    {
        titleSceneMenu.ChangeStatus = ChangeStatus;
        titleSceneExit.ChangeStatus = ChangeStatus;
    }
    private void Start()
    {
        S_InputSystem._instance.canInput = true;
        S_InputSystem._instance.SwitchActionMap(ActionMapKind.UI);

        _titleSceneStatus = TitleSceneStatus.menu;
    }

    private void FixedUpdate()
    {
        if (S_InputSystem._instance.isPushingSelect && !_selectPast) Select();
        else if (!S_InputSystem._instance.isPushingSelect && _selectPast) _selectPast = false;

        if (S_InputSystem._instance.isPushingCancel && !_cancelPast) Cancel();
        else if (!S_InputSystem._instance.isPushingCancel && _cancelPast) _cancelPast = false;

        if (S_InputSystem._instance.move == Vector2.up && !_upPast) Up();
        else if (S_InputSystem._instance.move != Vector2.up && _upPast) _upPast = false;

        if (S_InputSystem._instance.move == Vector2.down && !_downPast) Down();
        else if (S_InputSystem._instance.move != Vector2.down && _downPast) _downPast = false;

        if (S_InputSystem._instance.move == Vector2.left && !_leftPast) Left();
        else if (S_InputSystem._instance.move != Vector2.left && _leftPast) _leftPast = false;

        if (S_InputSystem._instance.move == Vector2.right && !_rightPast) Right();
        else if (S_InputSystem._instance.move != Vector2.right && _rightPast) _rightPast = false;
    }

    private void ChangeStatus(TitleSceneStatus status)
    {
        _titleSceneStatus = status;
    }

    private void Select()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
                titleSceneMenu.CursorSelect();
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
                titleSceneExit.CursorSelect();
            break;
        }

        _selectPast = true;
    }
    private void Cancel()
    {
        _cancelPast = true;
    }
    private void Up()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
                titleSceneMenu.CursorUp();
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
            break;
        }
        _upPast = true;
    }
    private void Down()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
                titleSceneMenu.CursorDown();
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
            break;
        }
        _downPast = true;
    }
    private void Left()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
                titleSceneExit.CursorLeft();
            break;
        }
        _leftPast = true;
    }
    private void Right()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
                titleSceneExit.CursorRight();
            break;
        }
        _rightPast = true;
    }
}
