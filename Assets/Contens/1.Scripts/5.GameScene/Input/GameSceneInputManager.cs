using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameSceneStatus { onPlay, menu, anyKey }

public class GameSceneInputManager : MonoBehaviour
{
    [SerializeField] GameSceneOnPlayInput gameSceneOnPlayInput;
    [SerializeField] GameSceneMenuInput gameSceneMenuInput;
    [SerializeField] GameSceneAnyKeyInput gameSceneAnyKeyInput;

    private GameSceneStatus _gameSceneStatus;

    private void Start()
    {
        S_InputSystem._instance.canInput = true;
        S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);

        _gameSceneStatus = GameSceneStatus.onPlay;
    }

    private void FixedUpdate()
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

    public void ChangeGameSceneStatus(GameSceneStatus gameSceneStatus)
    {
        _gameSceneStatus = gameSceneStatus;
        switch (_gameSceneStatus)
        {
            case GameSceneStatus.onPlay:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.Player);
            break;
            case GameSceneStatus.menu:
            case GameSceneStatus.anyKey:
                S_InputSystem._instance.SwitchActionMap(ActionMapKind.UI);
            break;
        }
    }
}
