using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSceneStatus { onPlay, menu, anyKey, clear }

public class GameSceneInputManager : MonoBehaviour
{
    [SerializeField] GameSceneOnPlayInput gameSceneOnPlayInput;
    [SerializeField] GameSceneMenuInput gameSceneMenuInput;
    [SerializeField] GameSceneAnyKeyInput gameSceneAnyKeyInput;
    [SerializeField] GameSceneClearInput gameSceneClearInput;

    [SerializeField] StageManager stageManager;

    private GameSceneStatus _gameSceneStatus;

    private void Awake()
    {
        stageManager.ChangeGameSceneStatus = ChangeGameSceneStatus;
    }

    private void Start()
    {
        
        #if UNITY_EDITOR
            S_InputSystem._instance.canInput = true;
        #endif

        S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
    }

    private void Update()
    {
        switch (_gameSceneStatus)
        {
            case GameSceneStatus.onPlay:
                gameSceneOnPlayInput.OnPlayInputUpdate();
            break;
            case GameSceneStatus.menu:
                gameSceneMenuInput.MenuInputUpdate();
            break;
            case GameSceneStatus.anyKey:
                gameSceneAnyKeyInput.AnyKeyInputUpdate();
            break;
            case GameSceneStatus.clear:
                gameSceneClearInput.ClearInputUpdate();
            break;
        }
    }

    private void ChangeGameSceneStatus(GameSceneStatus gameSceneStatus)
    {
        _gameSceneStatus = gameSceneStatus;
        switch (_gameSceneStatus)
        {
            case GameSceneStatus.onPlay:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
            break;
            case GameSceneStatus.menu:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.UI);
            break;
            case GameSceneStatus.anyKey:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
                gameSceneAnyKeyInput.Initialize();
            break;
            case GameSceneStatus.clear:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.UI);
            break;
        }
    }
}
