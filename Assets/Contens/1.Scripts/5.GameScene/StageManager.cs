using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] public PlayerDiePartsManager playerDiePartsManager;
    [SerializeField] PlayerExplosionAnimation playerExplosionAnimation;

    [SerializeField] SavePointManager savePointManager;
    [SerializeField] GearManager gearManager;
    [SerializeField] ActionCassetteManager actionCassetteManager;
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] BreakableBlockManager breakableBlockManager;
    [SerializeField] RecureCapsuleManager recureCapsuleManager;
    [SerializeField] WarpPointManager warpPointManager;
    [SerializeField] BackgroundManager backgroundManager;

    [SerializeField] DeathCountManager deathCountManager;
    [SerializeField] TimeManager timeManager;

    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] GameScenePauseMenu gameScenePauseMenu;
    [SerializeField] GameScenePauseUIToolkit gameScenePauseUIToolkit;

    public Action<GameSceneStatus> ChangeGameSceneStatus;
    public Action<GameSceneMenuStatus> ChangeGameSceneMenuStatus;

    private SceneKind _sceneKind;
    private GameSceneStatus _gameSceneStatusPast;

    private void Awake()
    {
        gearManager.gameSceneUI = gameSceneUI;

        _sceneKind = S_LoadSceneSystem._instance.GetCurrentSceneKind();
        gearManager._sceneKind = _sceneKind;
    }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        ChangeGameSceneStatus(GameSceneStatus.anyKey);

        savePointManager.TeleportStartPosition();
        playerManager.Initialize( savePointManager.savePoint.facingRight );

        deathCountManager.ResetDeathCount();
        timeManager.ResetTime();
        
        gameSceneUI.ChangeStageName(S_StageInfo._instance.stageDatas[_sceneKind].worldName, S_StageInfo._instance.stageDatas[_sceneKind].stageName);
        gameSceneUI.UpdateDeathCount( deathCountManager.deathCount );
        gameSceneUI.SwitchKidouUIVisible(true);
    }

    private void FixedUpdate()
    {
        gameSceneUI.ChangeTimeCount( timeManager.GetTimeString() );
    }

    //ーーー起動時の処理ーーー
    //このメソッド名がKidouなのは、僕の歴史やatmosphereによるものだ
    public void Kidou()
    {
        playerManager.isMovingPlayer = true;
        ChangeGameSceneStatus(GameSceneStatus.onPlay);

        timeManager.StartTimer();
        gameSceneUI.SwitchKidouUIVisible(false);

        S_SEManager._instance.Play("p_kidou");
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

        S_SEManager._instance.Play("p_explosion");

        yield return new WaitForSeconds(1.1f);

        S_SEManager._instance.Play("u_restart");
        S_FadeManager._instance.Fade(
            ()=>
            {
                savePointManager.TeleportRestartPosition();
                playerManager.Player.SetActive(true);

                gearManager.Initialize();
                actionCassetteManager.Initialize();
                buttonManager.Initialize();
                breakableBlockManager.Initialize();
                recureCapsuleManager.Initialize();
                warpPointManager.Initialize();
                backgroundManager.Initialize();

                deathCountManager.IncrementDeathCount();
                gameSceneUI.UpdateDeathCount( deathCountManager.deathCount );
                gameSceneUI.SwitchUIVisible(true);
            }, 
            ()=>
            {
                S_InputSystem._instance.canInput = true;
                ChangeGameSceneStatus(GameSceneStatus.anyKey);
            }, 
            FadeType.Diamond, 0.4f,0.1f,0.4f);  
    }

    //ーーードアに入る時の処理ーーー
    public void Door(SceneKind sceneKind)
    {
        StartCoroutine(CDoor(sceneKind));
    }
    IEnumerator CDoor(SceneKind sceneKind)
    {
        playerManager.Door();
        gearManager.OnSave();

        yield return new WaitForSeconds(0.2f);

        S_LoadSceneSystem._instance.LoadScene(sceneKind);

        yield return new WaitForSeconds(0.45f);

        S_SEManager._instance.Play("s_door");
    }

    //ーーークリア時の処理ーーー
    public void Clear()
    {
        StartCoroutine(CClear());
    }
    IEnumerator CClear()
    {
        yield return new WaitForSeconds(1);
    }

    //ーーーポーズ時の処理ーーー
    public void OpenPausePanel(GameSceneStatus currentStatus)
    {
        _gameSceneStatusPast = currentStatus;

        Time.timeScale = 0.0f;
        ChangeGameSceneStatus(GameSceneStatus.menu);
        ChangeGameSceneMenuStatus(GameSceneMenuStatus.pauseMenu);
        gameScenePauseUIToolkit.RootSetActive(true);
        gameScenePauseUIToolkit.MenuOptionsSelect(gameScenePauseMenu.menuIndex);
        gameScenePauseUIToolkit.OpenOrCloseConfirmPanel(false);
        S_SettingInfo._instance.OpenOrCloseSettingPanel(false);

        timeManager.StopTimer();

        S_SEManager._instance.Play("u_pause");
    }
    public void ClosePausePanel()
    {
        Time.timeScale = 1.0f;
        ChangeGameSceneStatus(_gameSceneStatusPast);
        gameScenePauseUIToolkit.RootSetActive(false);
        gameScenePauseUIToolkit.MenuOptionsUnSelect();

        timeManager.StartTimer();
    }
}
