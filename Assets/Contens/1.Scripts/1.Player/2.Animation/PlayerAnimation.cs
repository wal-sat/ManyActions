using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] TireAnimation tireAnimation;

    [SerializeField] PlayerSleepAnimation playerSleepAnimation;
    [SerializeField] PlayerZAnimation playerZAnimation;

    private bool _wasMoveingPast;

    public void Initialize()
    {
        tireAnimation.Initialize();

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

            tireAnimation.TireUpdate();
        }
        else
        {
            if (_wasMoveingPast)
            {
                playerSleepAnimation.SleepInitialize();
                playerZAnimation.ZInitialize();
            }

            playerSleepAnimation.SleepUpdate();
            playerZAnimation.ZUpdate();
        }

        _wasMoveingPast = isMovingPlayer;
    }
}
