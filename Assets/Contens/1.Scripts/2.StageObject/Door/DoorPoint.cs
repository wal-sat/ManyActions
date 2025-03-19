using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPoint : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GameObject ParticleBurst;
    [SerializeField] SceneKind sceneKind;

    private void Awake()
    {
        stageObjectCollisionArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        Instantiate(ParticleBurst, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, 5), Quaternion.Euler(-90, 0, 0));

        S_SEManager._instance.Play("s_savePoint");
        
        stageManager.Door(sceneKind);
    }
}
