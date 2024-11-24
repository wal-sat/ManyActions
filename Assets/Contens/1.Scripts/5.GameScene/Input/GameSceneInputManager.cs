using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSceneStatus { onPlay, menu, anyKey }

public class GameSceneInputManager : MonoBehaviour
{
    [SerializeField] GameSceneOnPlayInput gameSceneOnPlayInput;
    [SerializeField] GameSceneMenuInput gameSceneMenuInput;
    [SerializeField] GameSceneAnyKeyInput gameSceneAnyKeyInput;

    [SerializeField] StageManager stageManager;

    private GameSceneStatus _gameSceneStatus;

    private void Awake()
    {
        stageManager.ChangeGameSceneStatus = ChangeGameSceneStatus;
    }

    private void Start()
    {
        S_InputSystem._instance.canInput = true;
        S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);

        _gameSceneStatus = GameSceneStatus.onPlay;
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
            break;
        }
    }
}
