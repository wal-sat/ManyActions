using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] GameSceneUIUIToolkit gameSceneUIUIToolkit;

    public void UpdateDeathCount()
    {
        gameSceneUIUIToolkit.ChangeDeathLabel( S_GameInfo._instance.deathCount );
    }

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

    private void FixedUpdate()
    {
        gameSceneUIUIToolkit.ChangeTimeLabel( S_GameInfo._instance.GetMiniteAndSecond() );
    }

    public void MakeActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        gameSceneUIUIToolkit.MakeActionCard( isAcquired, actionName, actionIcon);
    }
}
