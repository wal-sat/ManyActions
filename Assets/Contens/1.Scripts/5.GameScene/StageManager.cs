using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerDiePartsManager playerDiePartsManager;
    [SerializeField] PlayerExplosionAnimation playerExplosionAnimation;


    [SerializeField] SavePointManager savePointManager;
    [SerializeField] GearManager gearManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] BreakableBlockManager breakableBlockManager;
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

    //ーーー起動時の処理ーーー
    //このメソッド名がKidouなのは、僕の歴史やatmosphereによるものだ
    public void Kidou()
    {
        playerManager.isMovingPlayer = true;
        ChangeGameSceneStatus(GameSceneStatus.onPlay);
    }

    //ーーーやられた時の処理ーーー
    public void Restart()
    {
        StartCoroutine(CRestart());     
    }
    IEnumerator CRestart()
    {
        S_InputSystem._instance.canInput = false;

        Vector3 playerPosition = playerManager.Player.transform.position;
        playerManager.Player.SetActive(false);

        playerExplosionAnimation.AnimationStart(playerPosition);
        
        playerManager.Initialize( savePointManager.savePoint.facingRight );
        
        playerDiePartsManager.Die(playerPosition);

        yield return new WaitForSeconds(1.1f);

        S_FadeManager._instance.Fade(
            ()=>{
                savePointManager.TeleportRestartPosition();
                playerManager.Player.SetActive(true);

                gearManager.Initialize();
                buttonManager.Initialize();
                breakableBlockManager.Initialize();
                }, 
            ()=>{
                S_InputSystem._instance.canInput = true;
                ChangeGameSceneStatus(GameSceneStatus.anyKey);
            }, 
            FadeType.Diamond, 0.4f,0.1f,0.4f);  
    }

    public void Door(SceneName sceneName)
    {
        playerManager.Door();
        gearManager.OnSave();

        S_LoadSceneSystem._instance.LoadScene(sceneName);
    }

    //ーーークリア時の処理ーーー
    public void Clear()
    {

    }

    //ーーーポーズ時の処理ーーー
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
}
