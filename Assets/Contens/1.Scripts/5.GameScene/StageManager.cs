using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] GameScenePauseUIToolkit gameScenePauseUIToolkit;

    public Action<GameSceneStatus> ChangeGameSceneStatus;
    public Action<GameSceneMenuStatus> ChangeGameSceneMenuStatus;

    private GameSceneStatus _gameSceneStatusPast;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        ChangeGameSceneStatus(GameSceneStatus.anyKey);

        savePointManager.TeleportStartPosition();
    }

    public void Restart()
    {
        bool facingRight = savePointManager.TeleportRestartPosition();
        
        playerManager.Initialize(facingRight);
        //爆発
        //フェード
        //開始処理
        ChangeGameSceneStatus(GameSceneStatus.anyKey);
    }

    public void Clear()
    {

    }

    public void OpenPausePanel(GameSceneStatus currentStatus)
    {
        _gameSceneStatusPast = currentStatus;

        Time.timeScale = 0.0f;
        ChangeGameSceneStatus(GameSceneStatus.menu);
        ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseMenu);
        gameScenePauseUIToolkit.RootSetActive(true);
        gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(false);
        gameScenePauseUIToolkit.OpenOrCloseSettingPanel(false);
    }
    public void ClosePausePanel()
    {
        Time.timeScale = 1.0f;
        ChangeGameSceneStatus(_gameSceneStatusPast);
        gameScenePauseUIToolkit.RootSetActive(false);
    }

    //このメソッド名がKidouなのは、僕の歴史やatmosphereによるものだ
    public void Kidou()
    {
        playerManager._isMovingPlayer = true;
        ChangeGameSceneStatus(GameSceneStatus.onPlay);
    }
}
