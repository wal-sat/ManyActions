using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStart : MonoBehaviour
{
    private void Start()
    {
        S_GameInfo._instance.onTimer = true;
    }
}
