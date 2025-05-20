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
    }

    [SerializeField] SavePointManager savePointManager;
    [SerializeField] Section[] sections;

    private int _carrentSectionIndex = 0;

    public SavePoint NextSection()
    {
        return ChangeSection(++_carrentSectionIndex);
    }

    public SavePoint ChangeSection(int sectionIndex)
    {
        _carrentSectionIndex = sectionIndex;

        for (int i = 0; i < sections.Length; i++) sections[i].section.SetActive(false);
        
        sections[sectionIndex].section.SetActive(true);

        return sections[sectionIndex].startPoint;
    }
}
