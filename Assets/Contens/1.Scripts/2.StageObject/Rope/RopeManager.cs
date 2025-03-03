using System.Collections;
using System.Collections.Generic;
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
        bool isOverlapRope = false;

        foreach (var rope in ropes)
        {
            if (rope.isOverlapRope) isOverlapRope = true;
        }

        return isOverlapRope;
    }
}
