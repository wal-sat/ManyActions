using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCountManager : MonoBehaviour
{
    public int deathCount { get; private set; }

    public void IncrementDeathCount()
    {
        deathCount ++;
    }
    public void ResetDeathCount()
    {
        deathCount = 0;
    }
}
