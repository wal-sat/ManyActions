using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAnyKeyInput : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    private bool _upPast;
    private bool _optionPast;

    public void Initialize()
    {
        
    }
    
    //GameSceneInputManagerのUpdate()から呼ばれる
    public void AnyKeyInputUpdate()
    {
        if (S_InputSystem._instance.direction == Vector2.up && !_upPast) AnyKey();
        else if (S_InputSystem._instance.direction != Vector2.up && _upPast) _upPast = false;

        if (S_InputSystem._instance.isPushingOption && !_optionPast) Option();
        else if (!S_InputSystem._instance.isPushingOption && _optionPast) _optionPast = false;
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
}
