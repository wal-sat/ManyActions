using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAreaManager : MonoBehaviour
{
    [SerializeField] SavePointManager savePointManager;
    private List<HiddenArea> hiddenAreas = new List<HiddenArea>();

    public void Register(HiddenArea hiddenArea)
    {
        hiddenAreas.Add(hiddenArea);
    }

    public void Lock()
    {
        foreach (var hiddenArea in hiddenAreas)
        {
           hiddenArea.isLockHiddenArea = true;
        }
    }

    public void Initialize()
    {
        foreach (var hiddenArea in hiddenAreas)
        {
            hiddenArea.Initialize();
            if (hiddenArea.savePoint == null || savePointManager.savePoint != hiddenArea.savePoint) hiddenArea.FirstCallCheckerReset();
        }
    }
}
