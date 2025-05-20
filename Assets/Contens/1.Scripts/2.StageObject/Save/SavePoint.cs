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
    [SerializeField] SpriteRenderer cameraArea;

    [HideInInspector] public Vector2 bottomLeftPos;
    [HideInInspector] public Vector2 topRightPos;

    private void Start()
    {
        if (stageObjectCollisionArea != null) stageObjectCollisionArea.triggerEnter = triggerEnter;

        if (cameraArea != null) 
        {
            Bounds bounds = cameraArea.bounds;
            bottomLeftPos = new Vector2(bounds.min.x, bounds.min.y);
            topRightPos = new Vector2(bounds.max.x, bounds.max.y);
            cameraArea.sprite = null;
        }
    }

    private void triggerEnter()
    {
        savePointManager.RegisterSavePoint(this);
        savePointView.OnSave();

        Instantiate(ParticleBurst, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, 5), Quaternion.Euler(-90, 0, 0));

        S_SEManager._instance.Play("s_savePoint");
    }
}
