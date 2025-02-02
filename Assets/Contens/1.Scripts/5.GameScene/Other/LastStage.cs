using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStage : MonoBehaviour
{
    private void Start()
    {
        S_GameInfo._instance.onTimer = false;
    }
}
