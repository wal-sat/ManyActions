using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenAreaManager : MonoBehaviour
{
    private List<HiddenArea> hiddenAreas = new List<HiddenArea>();

    public void Register(HiddenArea hiddenArea)
    {
        hiddenAreas.Add(hiddenArea);
    }

    public void Initialize()
    {
        foreach (var hiddenArea in hiddenAreas)
        {
            hiddenArea.Initialize();
        }
    }
}
