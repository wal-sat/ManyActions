using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Down_GoDown : PlayerActionBase
{
    private List<OneWayFloor> _oneWayFloors = new List<OneWayFloor>();

    public override void InitAction()
    {
        foreach (var oneWayFloor in _oneWayFloors)
        {
            oneWayFloor.onS_Down = true;
        }
    }
    public override void EndAction()
    {
        foreach (var oneWayFloor in _oneWayFloors)
        {
            oneWayFloor.onS_Down = false;
        }
    }
    public override void Initialize()
    {
        foreach (var oneWayFloor in _oneWayFloors)
        {
            oneWayFloor.onS_Down = false;
        }
    }

    public void Register(OneWayFloor oneWayFloor)
    {
        _oneWayFloors.Add(oneWayFloor);
    }
}
