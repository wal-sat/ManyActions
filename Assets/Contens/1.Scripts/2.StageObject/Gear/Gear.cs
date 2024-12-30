using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] GearManager gearManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GearView gearView;

    [HideInInspector] public GearStatus gearStatus;

    private void Awake()
    {
        gearManager.Register(this);
        stageObjectCollisionArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        gearView.SpriteChange(false);

        gearStatus = GearStatus.temporaryGet;
    }

    public void Initialize(GearStatus status)
    {
        gearStatus = status;

        if (gearStatus == GearStatus.acquired) gearView.SpriteChange(false);
        else if (gearStatus == GearStatus.unacquired) gearView.SpriteChange(true);
    }
}
