using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GearStatus { unacquired, temporaryGet, acquired }

public class GearManager : MonoBehaviour
{
    [HideInInspector] public GameSceneUI gameSceneUI;

    [HideInInspector] public SceneKind _sceneKind { private get; set; }
    private List<Gear> gears = new List<Gear>();

    private void Start()
    {
        Initialize();
    }

    public void Register(Gear gear)
    {
        gears.Add(gear);
    }

    public void OnGet()
    {
        gameSceneUI.ChangeGearCount( GetTemporaryGetGearCount() );
    }
    public void OnSave()
    {
        foreach (Gear gear in gears)
        {
            if (gear.gearStatus == GearStatus.temporaryGet)
            {
                gear.gearStatus = GearStatus.acquired;
                S_StageInfo._instance.stageDatas[_sceneKind].gearAcquire[gear.gearIndex] = true;
            }
        }

        gameSceneUI.ChangeGearCount( GetTemporaryGetGearCount() );
    }
    public void Initialize()
    {
        foreach (var gear in gears)
        {
            if (S_StageInfo._instance.stageDatas[_sceneKind].gearAcquire[gear.gearIndex]) gear.Initialize(GearStatus.acquired);
            else gear.Initialize(GearStatus.unacquired);
        }

        gameSceneUI.ChangeGearCount( GetTemporaryGetGearCount() );
    }

    private int GetTemporaryGetGearCount()
    {
        int count = 0;
        foreach (var gear in gears)
        {
            if (gear.gearStatus == GearStatus.temporaryGet) count ++;
        }
        return count;
    }
}
