using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] TireAnimation tireAnimation;

    public void Initialize()
    {
        tireAnimation.Initialize();
    }

    //PlayerManagerからFixedUpdateで呼ばれる
    public void AnimationUpdate()
    {
        tireAnimation.TireUpdate();
    }
}
