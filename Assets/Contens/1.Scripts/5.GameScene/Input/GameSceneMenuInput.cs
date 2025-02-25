using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSceneMenuStatus { pauseMenu, pauseSetting, pauseConfirm, clear }

public class GameSceneMenuInput : MonoBehaviour
{
    [SerializeField] GameScenePauseMenu gameScenePauseMenu;
    [SerializeField] GameScenePauseSetting gameScenePauseSetting;
    [SerializeField] GameScenePauseConfirm gameScenePauseConfirm;

    [SerializeField] StageManager stageManager;

    private bool _selectPast;
    private bool _cancelPast;
    private bool _upPast;
    private bool _downPast;
    private bool _leftPast;
    private bool _rightPast;

    private GameSceneMenuStatus _gameSceneMenuStatus;

    private void Awake()
    {
        gameScenePauseMenu.ChangeGameSceneMenuStatus = ChangeStatus;
        gameScenePauseSetting.ChangeGameSceneMenuStatus = ChangeStatus;
        gameScenePauseConfirm.ChangeGameSceneMenuStatus = ChangeStatus;

        stageManager.ChangeGameSceneMenuStatus = ChangeStatus;
    }

    public void Initialize()
    {
        
    }
    
    //GameSceneInputSystemのUpdate()から呼ばれる
    public void MenuInputUpdate()
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

    private void ChangeStatus(GameSceneMenuStatus status)
    {
        _gameSceneMenuStatus = status;
    }

    private void Select()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
                gameScenePauseMenu.CursorSelect();
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorSelect();
            break;
            case GameSceneMenuStatus.pauseConfirm:
                gameScenePauseConfirm.CursorSelect();
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _selectPast = true;
    }
    private void Cancel()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
                gameScenePauseMenu.CursorCancel();
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorCancel();
            break;
            case GameSceneMenuStatus.pauseConfirm:
                gameScenePauseConfirm.CursorCancel();
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _cancelPast = true;
    }
    private void Up()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
                gameScenePauseMenu.CursorUp();
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorUp();
            break;
            case GameSceneMenuStatus.pauseConfirm:
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _upPast = true;
    }
    private void Down()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
                gameScenePauseMenu.CursorDown();
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorDown();
            break;
            case GameSceneMenuStatus.pauseConfirm:
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _downPast = true;
    }
    private void Left()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorLeft();
            break;
            case GameSceneMenuStatus.pauseConfirm:
                gameScenePauseConfirm.CursorLeft();
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _leftPast = true;
    }
    private void Right()
    {
        switch (_gameSceneMenuStatus)
        {
            case GameSceneMenuStatus.pauseMenu:
            break;
            case GameSceneMenuStatus.pauseSetting:
                gameScenePauseSetting.CursorRight();
            break;
            case GameSceneMenuStatus.pauseConfirm:
                gameScenePauseConfirm.CursorRight();
            break;
            case GameSceneMenuStatus.clear:
            break;
        }
        _rightPast = true;
    }
}
