using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] SavePointView savePointView;
    [SerializeField] SavePointArea savePointArea;
    [SerializeField] public int savePointIndex;
    [SerializeField] public bool facingRight;

    private void Awake()
    {
        savePointArea.triggerEnter = triggerEnter;
    }

    private void triggerEnter()
    {
        savePointManager.RegisterSavePoint(this);
        savePointView.OnSave();
    }
}
