using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearStatus { unacquired, temporaryGet, acquired }

public class GearManager : MonoBehaviour
{
    [SerializeField] SceneName sceneName;
    private List<Gear> gears = new List<Gear>();

    private void Start()
    {
        S_GameInfo._instance.InstantiateGearInfo(sceneName, gears.Count);

        Initialize();
    }

    public void Register(Gear gear)
    {
        gears.Add(gear);
    }

    public void OnSave()
    {
        for (int i = 0; i < gears.Count; i++)
        {
            if (gears[i].gearStatus == GearStatus.temporaryGet)
            {
                gears[i].gearStatus = GearStatus.acquired;
                S_GameInfo._instance.RegisterGearInfo(sceneName, i, true);
            }
        }
    }
    public void Initialize()
    {
        bool[] gearInfo = S_GameInfo._instance.gearInfo[sceneName];

        for (int i = 0; i < gearInfo.Length; i++)
        {
            Debug.Log(gearInfo[i]);
        }

        for (int i = 0; i < gearInfo.Length; i++)
        {
            if (gearInfo[i]) gears[i].Initialize(GearStatus.acquired);
            else if (!gearInfo[i]) gears[i].Initialize(GearStatus.unacquired);
        }
    }
}
