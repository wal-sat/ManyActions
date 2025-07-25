using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] GearManager gearManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GearView gearView;
    [SerializeField] GameObject ParticleDefault;
    [SerializeField] GameObject ParticleBurst;
    [SerializeField] public int gearIndex;

    [HideInInspector] public GearStatus gearStatus;

    private void Awake()
    {
        if (gearManager != null) gearManager.Register(this);
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
    }

    private void TriggerEnter()
    {
        if (gearStatus == GearStatus.unacquired)
        {
            gearView.SpriteChange(false);

            gearStatus = GearStatus.temporaryGet;

            if (gearManager != null) gearManager.OnGet();

            ParticleDefault.SetActive(false);
            Instantiate(ParticleBurst, new Vector3(this.transform.position.x, this.transform.position.y, 5), Quaternion.identity);

            S_SEManager._instance.Play("s_getGear");
        }

    }

    public void Initialize(GearStatus status)
    {
        gearStatus = status;

        if (gearStatus == GearStatus.acquired) 
        {
            gearView.SpriteChange(false);
            ParticleDefault.SetActive(false);
        }
        else if (gearStatus == GearStatus.unacquired) 
        {
            gearView.SpriteChange(true);
            ParticleDefault.SetActive(true);
        }
    }
}
