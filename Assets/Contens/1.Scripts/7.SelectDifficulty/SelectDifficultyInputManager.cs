using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectDifficultySceneStatus { menu, confirm }
public enum DifficultyLevel { normal, extra }


public class SelectDifficultyInputManager : MonoBehaviour
{
    [SerializeField] SelectDifficultyMenu selectDifficultyMenu;
    [SerializeField] SelectDifficultyConfirm selectDifficultyConfirm;

    private SelectDifficultySceneStatus _selectDifficultySceneStatus;
    private DifficultyLevel _difficultyLevel;

    private bool _selectPast;
    private bool _cancelPast;
    private bool _upPast;
    private bool _downPast;
    private bool _leftPast;
    private bool _rightPast;

    private void Awake()
    {
        selectDifficultyMenu.ChangeStatus = ChangeStatus;
        selectDifficultyMenu.ChangeDifficultyLevel = ChangeDifficultyLevel;
        selectDifficultyConfirm.ChangeStatus = ChangeStatus;
    }
    private void Start()
    {
        S_InputSystem._instance.canInput = true;
        S_InputSystem._instance.SwitchActionMap(ActionMapKind.UI);
        
        _selectDifficultySceneStatus = SelectDifficultySceneStatus.menu;
    }

    private void Update()
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

    private void ChangeStatus(SelectDifficultySceneStatus status)
    {
        _selectDifficultySceneStatus = status;
    }
    private void ChangeDifficultyLevel(DifficultyLevel level)
    {
        _difficultyLevel = level;
    }

    private void Select()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorSelect();
            break;
            case SelectDifficultySceneStatus.confirm:
                selectDifficultyConfirm.CursorSelect(_difficultyLevel);
            break;
        }
        _selectPast = true;
    }
    private void Cancel()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorCancel();
            break;
            case SelectDifficultySceneStatus.confirm:
                selectDifficultyConfirm.CursorCancel(_difficultyLevel);
            break;
        }
        _cancelPast = true;
    }
    private void Up()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorUp();
            break;
            case SelectDifficultySceneStatus.confirm:
            break;
        }
        _upPast = true;
    }
    private void Down()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorDown();
            break;
            case SelectDifficultySceneStatus.confirm:
            break;
        }
        _downPast = true;
    }
    private void Left()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorLeft();
            break;
            case SelectDifficultySceneStatus.confirm:
                selectDifficultyConfirm.CursorLeft();
            break;
        }
        _leftPast = true;
    }
    private void Right()
    {
        switch (_selectDifficultySceneStatus)
        {
            case SelectDifficultySceneStatus.menu:
                selectDifficultyMenu.CursorRight();
            break;
            case SelectDifficultySceneStatus.confirm:
                selectDifficultyConfirm.CursorRight();
            break;
        }
        _rightPast = true;
    }
}
