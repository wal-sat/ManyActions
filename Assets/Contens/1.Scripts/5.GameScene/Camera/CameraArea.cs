using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArea : MonoBehaviour
{
    [SerializeField] CameraAreaManager cameraAreaManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] public int cameraAreaIndex;
    [SerializeField] public float cameraSize;

    [HideInInspector] public Vector2 bottomLeftPos;
    [HideInInspector] public Vector2 topRightPos;

    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Bounds bounds = sr.bounds;

        bottomLeftPos = new Vector2(bounds.min.x, bounds.min.y);
        topRightPos = new Vector2(bounds.max.x, bounds.max.y);
        sr.sprite = null;

        stageObjectCollisionArea.triggerEnter = TriggerEnter;
        stageObjectCollisionArea.triggerExit = TriggerExit;
    }

    private void TriggerEnter()
    {
        cameraAreaManager.Register(this);   
    }
    private void TriggerExit()
    {
        cameraAreaManager.Unregister(this);
    }
}
