using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GearStatus { unacquired, temporaryGet, acquired }

public class GearManager : MonoBehaviour
{
    [SerializeField] GameSceneUI gameSceneUI;

    private SceneName _sceneName;
    private List<Gear> gears = new List<Gear>();

    private void Start()
    {
        _sceneName = S_LoadSceneSystem._instance.StringToSceneName( SceneManager.GetActiveScene().name );
        S_GameInfo._instance.InstantiateGearInfo(_sceneName, gears.Count);

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
        for (int i = 0; i < gears.Count; i++)
        {
            if (gears[i].gearStatus == GearStatus.temporaryGet)
            {
                gears[i].gearStatus = GearStatus.acquired;
                S_GameInfo._instance.RegisterGearInfo(_sceneName, i, true);
            }
        }

        gameSceneUI.ChangeGearCount( GetTemporaryGetGearCount() );
    }
    public void Initialize()
    {
        bool[] gearInfo = S_GameInfo._instance.gearInfo[_sceneName];

        for (int i = 0; i < gearInfo.Length; i++)
        {
            if (gearInfo[i]) gears[i].Initialize(GearStatus.acquired);
            else if (!gearInfo[i]) gears[i].Initialize(GearStatus.unacquired);
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
