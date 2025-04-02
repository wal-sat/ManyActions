using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    [Serializable] class Section
    {
        [SerializeField] public GameObject section;
        [SerializeField] public SavePoint startPoint;
        [SerializeField] public float cameraSize;
        [SerializeField] public Vector2 bottomLeftPos;
        [SerializeField] public Vector2 topRightPos;
    }

    [SerializeField] SavePointManager savePointManager;
    [SerializeField] GameObject virtualCamera;
    [SerializeField] Section[] sections;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private LockAxisCamera _lockAxisCamera;

    private int _carrentSectionIndex = 0;

    private void Awake()
    {
        _cinemachineVirtualCamera = virtualCamera.GetComponent<CinemachineVirtualCamera>();
        _lockAxisCamera = virtualCamera.GetComponent<LockAxisCamera>();
    }

    public void NextSection()
    {
        ChangeSection(++_carrentSectionIndex);
    }

    public void ChangeSection(int sectionIndex)
    {
        _carrentSectionIndex = sectionIndex;

        for (int i = 0; i < sections.Length; i++) sections[i].section.SetActive(false);
        
        sections[sectionIndex].section.SetActive(true);
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = sections[sectionIndex].cameraSize;
        _lockAxisCamera.SetMoveRange(sections[sectionIndex].bottomLeftPos, sections[sectionIndex].topRightPos);

        savePointManager.RegisterSavePoint(sections[sectionIndex].startPoint, true);
    }
}
