using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GameObject ParticleBurst;

    private void Awake()
    {
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
    }

    private void TriggerEnter()
    {
        Instantiate(ParticleBurst, new Vector3(this.transform.position.x, this.transform.position.y, 5), Quaternion.Euler(-90, 0, 0));

        stageManager.Clear();
    }
}
