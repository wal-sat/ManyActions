using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    [SerializeField] HeartBeatAreaManager heartBeatAreaManager;
    [SerializeField] HiddenAreaManager hiddenAreaManager;
    [SerializeField] LRBlockManager lRBlockManager;
    [SerializeField] RecureCapsuleManager recureCapsuleManager;
    [SerializeField] WarpPointManager warpPointManager;
    [SerializeField] BackgroundManager backgroundManager;

    [SerializeField] SectionManager sectionManager;
    [SerializeField] DeathCountManager deathCountManager;
    [SerializeField] TimeManager timeManager;
    [SerializeField] CameraManager cameraManager;

    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] GameScenePauseMenu gameScenePauseMenu;
    [SerializeField] GameScenePauseUIToolkit gameScenePauseUIToolkit;
    [SerializeField] GameSceneClearUIToolkit gameSceneClearUIToolkit;

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
        Time.timeScale = 1;
        S_BGMManager._instance.Play("stage", 1.5f);

        ChangeGameSceneStatus(GameSceneStatus.anyKey);

        savePointManager.TeleportStartPosition( sectionManager.ChangeSection(0) );
        playerManager.Initialize( savePointManager.savePoint.facingRight );

        deathCountManager.ResetDeathCount();
        timeManager.ResetTime();
        
        gameSceneUI.ChangeStageName(S_StageInfo._instance.stageDatas[_sceneKind].worldName, S_StageInfo._instance.stageDatas[_sceneKind].stageName);
        gameSceneUI.UpdateDeathCount( deathCountManager.deathCount );
        gameSceneUI.SwitchKidouUIVisible(true);
        gameSceneClearUIToolkit.RootVisible(false);
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

        cameraManager.cameraBodyChanger.EnableDumping(true);

        timeManager.StartTimer();
        gameSceneUI.SwitchKidouUIVisible(false);

        S_SEManager._instance.Play("p_kidou");
    }

    //ーーーやられた時の処理ーーー
    public void Restart(float angleZ = 0)
    {
        StartCoroutine(CRestart(angleZ));     
    }
    IEnumerator CRestart(float angleZ = 0)
    {
        S_InputSystem._instance.canInput = false;

        heartBeatAreaManager.Lock();
        hiddenAreaManager.Lock();

        Vector3 playerPosition = playerManager.Player.transform.position;
        playerManager.Player.SetActive(false);

        playerExplosionAnimation.AnimationStart(playerPosition);
        
        playerManager.Initialize( savePointManager.savePoint.facingRight );
        
        playerDiePartsManager.Die(playerPosition, angleZ);

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
                heartBeatAreaManager.Initialize();
                hiddenAreaManager.Initialize();
                lRBlockManager.Initialize();
                recureCapsuleManager.Initialize();
                warpPointManager.Initialize();
                backgroundManager.Initialize();

                cameraManager.cameraBodyChanger.EnableDumping(false);

                deathCountManager.IncrementDeathCount();
                gameSceneUI.UpdateDeathCount( deathCountManager.deathCount );
                gameSceneUI.SwitchUIVisible(true);
            }, 
            ()=>
            {
                S_InputSystem._instance.canInput = true;
                ChangeGameSceneStatus(GameSceneStatus.anyKey);
            }, 
            FadeType.Diamond, 0.31f,0.08f,0.31f);  
    }

    //ーーードアに入る時の処理ーーー
    public void Door()
    {
        StartCoroutine(CDoor());
    }
    IEnumerator CDoor()
    {
        S_InputSystem._instance.canInput = false;

        playerManager.SectionClear();
        gearManager.OnSave();
        timeManager.StopTimer();
        
        yield return new WaitForSeconds(0.2f);

        S_FadeManager._instance.Fade(
            () => {
                savePointManager.TeleportStartPosition( sectionManager.NextSection() );
                playerManager.Initialize( savePointManager.savePoint.facingRight );

                gameSceneUI.SwitchKidouUIVisible(true);
            },
            () => {
                S_InputSystem._instance.canInput = true;
                ChangeGameSceneStatus(GameSceneStatus.anyKey);
            },
            FadeType.Black, 0.5f, 1f, 0.5f
        );

        yield return new WaitForSeconds(0.65f);

        S_SEManager._instance.Play("s_door");
    }

    //ーーークリア時の処理ーーー
    public void Clear()
    {
        StartCoroutine(CClear());
    }
    IEnumerator CClear()
    {
        S_InputSystem._instance.canInput = false;
        S_BGMManager._instance.Play("clear", 1f);
        S_SEManager._instance.Play("s_getKey");
        S_AmbientSoundManager._instance.Stop("heartBeat", 0.5f);

        DOVirtual.Float(1f, 0f, 0.5f, value => Time.timeScale = value).SetEase(Ease.OutCubic).SetUpdate(true);

        ChangeGameSceneStatus(GameSceneStatus.clear);
        
        timeManager.StopTimer();
        gearManager.OnSave();

        S_StageInfo._instance.stageDatas[_sceneKind].isClear = true;
        S_StageInfo._instance.stageDatas[_sceneKind].SetDeathCount(deathCountManager.deathCount, true);
        S_StageInfo._instance.stageDatas[_sceneKind].SetPlayTime(timeManager.GetTime(), true);

        for (int i = 0; i < 5; i++) gameSceneClearUIToolkit.GearIconAcquired(i, S_StageInfo._instance.stageDatas[_sceneKind].gearAcquire[i]);
        gameSceneClearUIToolkit.ChangeClearIcon(S_StageInfo._instance.GetClearIcon(S_StageInfo._instance.stageDatas[_sceneKind].clearKind));
        gameSceneClearUIToolkit.ChangeMessageLabel(S_StageInfo._instance.GetClearMessage(S_StageInfo._instance.stageDatas[_sceneKind].clearKind));
        gameSceneClearUIToolkit.ChangeDeathCountLabel(deathCountManager.deathCount);
        gameSceneClearUIToolkit.ChangeMinimumDeathCountLabel(S_StageInfo._instance.stageDatas[_sceneKind].minimumDeathCount);
        gameSceneClearUIToolkit.ChangeClearTimeLabel(timeManager.GetTimeString());
        gameSceneClearUIToolkit.ChangeFastestClearTimeLabel(S_StageInfo._instance.stageDatas[_sceneKind].GetFastestClearTimeString());

        yield return new WaitForSecondsRealtime(0.5f);

        playerManager.SectionClear();
        
        gameSceneClearUIToolkit.RootVisible(true);

        yield return new WaitForSecondsRealtime(0.5f);

        S_InputSystem._instance.canInput = true;
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
