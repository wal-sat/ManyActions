using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageSelectSceneStatus { menu, option }

public class StageSelectInputManager : MonoBehaviour
{
    [SerializeField] StageSelectMenu stageSelectMenu;
    [SerializeField] StageSelectOption stageSelectOption;

    private StageSelectSceneStatus _stageSelectSceneStatus;

    private bool _southPast;
    private bool _eastPast;
    private bool _westPast;
    private bool _northPast;
    private bool _upPast;
    private bool _downPast;
    private bool _leftPast;
    private bool _rightPast;
    private bool _LPast;
    private bool _RPast;
    private bool _optionPast;

    private void Awake()
    {
        stageSelectMenu.ChangeStatus = ChangeStatus;
        stageSelectOption.ChangeStatus = ChangeStatus;
    }

    private void Start()
    {
        #if UNITY_EDITOR
            S_InputSystem._instance.canInput = true;
        #endif

        S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
        
        _stageSelectSceneStatus = StageSelectSceneStatus.menu;
    }

    private void Update()
    {
        if (S_InputSystem._instance.isPushingSouth && !_southPast) South();
        else if (!S_InputSystem._instance.isPushingSouth && _southPast) _southPast = false;

        if (S_InputSystem._instance.isPushingEast && !_eastPast) East();
        else if (!S_InputSystem._instance.isPushingEast && _eastPast) _eastPast = false;

        if (S_InputSystem._instance.isPushingWest && !_westPast) West();
        else if (!S_InputSystem._instance.isPushingWest && _westPast) _westPast = false;

        if (S_InputSystem._instance.isPushingNorth && !_northPast) North();
        else if (!S_InputSystem._instance.isPushingNorth && _northPast) _northPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.up && !_upPast) Up();
        else if (S_InputSystem._instance.leftDirection != Vector2.up && _upPast) _upPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.down && !_downPast) Down();
        else if (S_InputSystem._instance.leftDirection != Vector2.down && _downPast) _downPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.left && !_leftPast) Left();
        else if (S_InputSystem._instance.leftDirection != Vector2.left && _leftPast) _leftPast = false;

        if (S_InputSystem._instance.leftDirection == Vector2.right && !_rightPast) Right();
        else if (S_InputSystem._instance.leftDirection != Vector2.right && _rightPast) _rightPast = false;

        if (S_InputSystem._instance.isPushingL && !_LPast) L();
        else if (!S_InputSystem._instance.isPushingL && _LPast) _LPast = false;

        if (S_InputSystem._instance.isPushingR && !_RPast) R();
        else if (!S_InputSystem._instance.isPushingR && _RPast) _RPast = false;

        if (S_InputSystem._instance.isPushingOption && !_optionPast) Option();
        else if (!S_InputSystem._instance.isPushingOption && _optionPast) _optionPast = false;
    }

    private void ChangeStatus(StageSelectSceneStatus status)
    {
        _stageSelectSceneStatus = status;
    }

    private void South()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorSelect();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorSelect();
                break;
        }
        _southPast = true;
    }
    private void East()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorCancel();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorCancel();
                break;
        }
        _eastPast = true;
    }
    private void West()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.West();
                break;
        }
        _westPast = true;
    }
    private void North()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.North();
                break;
        }
        _northPast = true;
    }
    private void Up()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorUp();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorUp();
                break;
        }
        _upPast = true;
    }
    private void Down()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.CursorDown();
                break;
            case StageSelectSceneStatus.option:
                stageSelectOption.CursorDown();
                break;
        }
        _downPast = true;
    }
    private void Left()
    {
        switch (_stageSelectSceneStatus)
        {

        }
        _leftPast = true;
    }
    private void Right()
    {
        switch (_stageSelectSceneStatus)
        {

        }
        _rightPast = true;
    }
    private void L()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.L();
                break;
        }
        _LPast = true;
    }
    private void R()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.R();
                break;
        }
        _RPast = true;
    }
    private void Option()
    {
        switch (_stageSelectSceneStatus)
        {
            case StageSelectSceneStatus.menu:
                stageSelectMenu.Option();
                break;
        }
        _optionPast = true;
    }
}
