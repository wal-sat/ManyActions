using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecureCapsuleManager : MonoBehaviour
{
    private List<RecureCapsule> _recureCapsules = new List<RecureCapsule>();

    public void Register(RecureCapsule recureCapsule)
    {
        _recureCapsules.Add(recureCapsule);
    }

    public void Initialize()
    {
        foreach (var recureCapsule in _recureCapsules)
        {
            recureCapsule.Initialize();
        }
    }
}
