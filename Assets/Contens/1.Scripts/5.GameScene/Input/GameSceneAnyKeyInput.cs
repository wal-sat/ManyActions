using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnyKeyStatus { ready, observe }

public class GameSceneAnyKeyInput : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] CameraManager cameraManager;
    [SerializeField] GameSceneUI gameSceneUI;

    private AnyKeyStatus anyKeyStatus;
    private Vector2 _direction;
    private bool _upPast;
    private bool _optionPast;
    private bool _eastPast;
    private bool _southPast;

    public void Initialize()
    {
        anyKeyStatus = AnyKeyStatus.ready;
        cameraManager.ChangeCamera(CameraKind.main);
    }
    
    //GameSceneInputManagerのUpdate()から呼ばれる
    public void AnyKeyInputUpdate()
    {
        _direction = S_InputSystem._instance.rightDirection;
        
        switch (anyKeyStatus)
        {
            case AnyKeyStatus.ready:
            {
                if (_direction != Vector2.zero) InitObserve();

                if (S_InputSystem._instance.leftDirection == Vector2.up && !_upPast) AnyKey();
                else if (S_InputSystem._instance.leftDirection != Vector2.up && _upPast) _upPast = false;

                if (S_InputSystem._instance.isPushingSouth && !_southPast) DestroyDieParts();
                else if (!S_InputSystem._instance.isPushingSouth && _southPast) _southPast = false;
            }
            break;
            case AnyKeyStatus.observe:
            {
                cameraManager.sleepingCameraMovement.SleepCameraMove(_direction);
                
                if (S_InputSystem._instance.isPushingEast && !_eastPast) Cancel();
                else if (!S_InputSystem._instance.isPushingEast && _eastPast) _eastPast = false;
            }
            break;
        }        

        if (S_InputSystem._instance.isPushingOption && !_optionPast) Option();
        else if (!S_InputSystem._instance.isPushingOption && _optionPast) _optionPast = false;
    }

    private void InitObserve()
    {
        anyKeyStatus = AnyKeyStatus.observe;

        Vector2 savePointPos = new Vector2(savePointManager.savePoint.transform.position.x, savePointManager.savePoint.transform.position.y);
        cameraManager.sleepingCameraMovement.SleepCameraInit(savePointPos, savePointManager.savePoint.bottomLeftPos, savePointManager.savePoint.topRightPos);
        cameraManager.ChangeCamera(CameraKind.sleep);

        gameSceneUI.SwitchUIVisible(false);
    }

    private void DestroyDieParts()
    {
        stageManager.playerDiePartsManager.DestroyDieParts();
        _southPast = true;

        S_SEManager._instance.Play("u_back");
    }

    private void AnyKey()
    {
        stageManager.Kidou();
        _upPast = true;
    }
    private void Option()
    {
        stageManager.OpenPausePanel(GameSceneStatus.anyKey);
        _optionPast = true;
    }
    
    private void Cancel()
    {
        anyKeyStatus = AnyKeyStatus.ready;

        cameraManager.ChangeCamera(CameraKind.main);
        gameSceneUI.SwitchUIVisible(true);

        _eastPast = true;
    }
}
