using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] PlayerTireAnimation playerTireAnimation;

    [SerializeField] PlayerSleepAnimation playerSleepAnimation;
    [SerializeField] PlayerZAnimation playerZAnimation;

    private bool _wasMoveingPast;

    public void Initialize()
    {
        playerTireAnimation.Initialize();
        playerSleepAnimation.SleepInitialize();
        playerZAnimation.ZInitialize();

        _wasMoveingPast = true;
    }

    //PlayerManagerからFixedUpdateで呼ばれる
    public void AnimationUpdate(bool isMovingPlayer)
    {
        if (isMovingPlayer)
        {
            if (!_wasMoveingPast)
            {
                playerSleepAnimation.SleepEnd();
                playerZAnimation.ZEnd();
            }

            playerTireAnimation.TireUpdate();
        }
        else
        {
            playerSleepAnimation.SleepUpdate();
            playerZAnimation.ZUpdate();
        }

        _wasMoveingPast = isMovingPlayer;
    }
}
