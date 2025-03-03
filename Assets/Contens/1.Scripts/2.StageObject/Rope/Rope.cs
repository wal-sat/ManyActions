using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] RopeManager ropeManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;

    [HideInInspector] public bool isOverlapRope;

    private void Awake()
    {
        ropeManager.Register(this);

        stageObjectCollisionArea.triggerEnter = TriggerEnter;
        stageObjectCollisionArea.triggerExit = TriggerExit;
    }

    private void TriggerEnter()
    {
        isOverlapRope = true;
    }
    private void TriggerExit()
    {
        isOverlapRope = false;
    }
}
