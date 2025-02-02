using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] SavePointView savePointView;
    [SerializeField] GameObject safetyArea;
    [SerializeField] public StageActionData stageActionData;
    [SerializeField] public int savePointIndex;
    [SerializeField] public bool facingRight;

    private void Awake()
    {
        if (stageObjectCollisionArea != null) stageObjectCollisionArea.triggerEnter = triggerEnter;

        safetyArea.SetActive(false);
    }

    private void triggerEnter()
    {
        savePointManager.RegisterSavePoint(this);
        savePointView.OnSave();

        S_SEManager._instance.Play("s_savePoint");
    }

    public void SetActiveSafetyArea(bool isActive)
    {
        safetyArea.SetActive(isActive);
    }
}
