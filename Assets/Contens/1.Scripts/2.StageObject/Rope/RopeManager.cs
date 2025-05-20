using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    private List<Rope> ropes = new List<Rope>();

    public void Register(Rope rope)
    {
        ropes.Add(rope);
    }

    public bool IsOverlapRope()
    {
        return ropes.Any(item => item.isOverlapRope);
    }

    public Transform GetRopeTransform()
    {
        return ropes.First(item => item.isOverlapRope).gameObject.transform;
    }
}
