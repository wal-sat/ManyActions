using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TitleSceneStatus { menu, setting, exit }

public class TitleSceneInputManager : MonoBehaviour
{
    [SerializeField] TitleSceneMenu titleSceneMenu;
    [SerializeField] TitleSceneSetting titleSceneSetting;
    [SerializeField] TitleSceneExit titleSceneExit;

    private TitleSceneStatus _titleSceneStatus;

    private void FixedUpdate()
    {
        if (S_InputSystem._instance.isPushingSelect) Select();
        else if (S_InputSystem._instance.isPushingCancel) Cancel();
        else if (S_InputSystem._instance.move == Vector2.up) Up();
        else if (S_InputSystem._instance.move == Vector2.down) Down();
        else if (S_InputSystem._instance.move == Vector2.left) Left();
        else if (S_InputSystem._instance.move == Vector2.right) Right();
    }

    private void Select()
    {
        switch (_titleSceneStatus)
        {
            case TitleSceneStatus.menu:
            break;
            case TitleSceneStatus.setting:
            break;
            case TitleSceneStatus.exit:
            break;
        }
    }
    private void Cancel()
    {

    }
    private void Up()
    {

    }
    private void Down()
    {

    }
    private void Left()
    {

    }
    private void Right()
    {

    }
}
