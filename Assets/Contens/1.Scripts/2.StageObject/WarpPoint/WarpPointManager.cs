using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPointManager : MonoBehaviour
{
    private List<WarpPoint> _warpPoints = new List<WarpPoint>();

    public void Register(WarpPoint warpPoint)
    {
        _warpPoints.Add(warpPoint);
    }

    public void Initialize()
    {
        foreach (var warpPoint in _warpPoints)
        {
            warpPoint.Initialize();
        }
    }
}
