using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool _onTimer;
    private float _time;

    private void FixedUpdate()
    {
        if (_onTimer) _time += Time.deltaTime;
    }

    public void StartTimer()
    {
        if (!_onTimer) _onTimer = true;
    }
    public void StopTimer()
    {
        _onTimer = false;
    }
    public int GetTime()
    {
        return (int) _time;
    }
    public string GetTimeString()
    {
        int hours = (int) _time / 3600;
        int minutes = (int) (_time % 3600) / 60;
        int seconds = (int) _time % 60;

        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
    public void ResetTime()
    {
        _time = 0;
    }
}
