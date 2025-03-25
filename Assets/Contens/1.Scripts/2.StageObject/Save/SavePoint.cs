using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] SavePointView savePointView;
    [SerializeField] GameObject ParticleBurst;
    [SerializeField] public AcquireActionData acquireActionData;
    [SerializeField] public int savePointIndex;
    [SerializeField] public bool facingRight;
    [Header("x:Minus, y:Plus")] [SerializeField] public Vector2 sleepMoveDistance;

    private void Awake()
    {
        if (stageObjectCollisionArea != null) stageObjectCollisionArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        savePointManager.RegisterSavePoint(this);
        savePointView.OnSave();

        Instantiate(ParticleBurst, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, 5), Quaternion.Euler(-90, 0, 0));

        S_SEManager._instance.Play("s_savePoint");
    }
}
