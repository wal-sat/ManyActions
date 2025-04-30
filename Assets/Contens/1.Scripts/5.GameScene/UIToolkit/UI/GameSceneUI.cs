using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] GameSceneUIUIToolkit gameSceneUIUIToolkit;

    public void UpdateDeathCount(int deathCount)
    {
        gameSceneUIUIToolkit.ChangeDeathLabel( deathCount );
    }

    public void ChangeGearCount(int temporaryGetCount)
    {
        gameSceneUIUIToolkit.ChangeGearLabel( S_GameInfo._instance.GetGearCountInAScene( S_LoadSceneSystem._instance.GetCurrentSceneKind() ) );

        if (temporaryGetCount == 0) gameSceneUIUIToolkit.SetActiveGearPlusLabel(false);
        else 
        {
            gameSceneUIUIToolkit.SetActiveGearPlusLabel(true);
            gameSceneUIUIToolkit.ChangeGearPlusLabel( temporaryGetCount );
        }
    }

    public void ChangeStageName(string worldName, string stageName)
    {
        gameSceneUIUIToolkit.ChangeStageNameLabel( worldName, stageName );
    }

    public void ChangeTimeCount(string timeString)
    {
        gameSceneUIUIToolkit.ChangeTimeLabel(timeString);
    }

    public void MakeActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        gameSceneUIUIToolkit.MakeActionCard( isAcquired, actionName, actionIcon);
    }

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

    //ーーーーーデバック用ーーーーー
    public void Debug_UIInvisible()
    {
        gameSceneUIUIToolkit.RootFade(false);
        gameSceneUIUIToolkit.KidouUIFade(false);
        gameSceneUIUIToolkit.SleepCameraUIFade(false);
    }
}
