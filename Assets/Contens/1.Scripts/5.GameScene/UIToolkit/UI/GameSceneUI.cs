using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] GameSceneUIUIToolkit gameSceneUIUIToolkit;

    //ーーーデス数ーーー
    public void UpdateDeathCount()
    {
        gameSceneUIUIToolkit.ChangeDeathLabel( S_GameInfo._instance.deathCount );
    }

    //ーーーギアーーー
    public void ChangeGearCount(int temporaryGetCount)
    {
        gameSceneUIUIToolkit.ChangeGearLabel( S_GameInfo._instance.GetGearCount() );

        if (temporaryGetCount == 0) gameSceneUIUIToolkit.SetActiveGearPlusLabel(false);
        else 
        {
            gameSceneUIUIToolkit.SetActiveGearPlusLabel(true);
            gameSceneUIUIToolkit.ChangeGearPlusLabel( temporaryGetCount );
        }
    }

    //ーーー時間ーーー
    private void FixedUpdate()
    {
        gameSceneUIUIToolkit.ChangeTimeLabel( S_GameInfo._instance.GetMiniteAndSecond() );
    }

    //ーーーアクションカードーーー
    public void MakeActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        gameSceneUIUIToolkit.MakeActionCard( isAcquired, actionName, actionIcon);
    }

    //ーーーアクション回数ーーー
    public void ChangeActionCount(ActionKind actionKind, int count)
    {
        gameSceneUIUIToolkit.ChangeActionCountLabel( actionKind, count );
    }
    public void VisibleActionCount(ActionKind actionKind, bool isVisible)
    {
        gameSceneUIUIToolkit.VisibleActionCountLabel( actionKind, isVisible );
    }

    //ーーーUI全体ーーー
    public void SwitchUIVisible(bool isVisible)
    {
        gameSceneUIUIToolkit.RootFade(isVisible);
        gameSceneUIUIToolkit.KidouUIFade(isVisible);
        gameSceneUIUIToolkit.SleepCameraUIFade(!isVisible);
    }
    public void SwitchKidouUIVisible(bool isVisible)
    {
        gameSceneUIUIToolkit.KidouUIFade(isVisible);
    }
}
