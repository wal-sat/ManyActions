using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StageSelectOptionConfirmUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _panel;
    private VisualElement[] _optionLabels = new VisualElement[2];

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _panel = root.Q<VisualElement>("Panel");
        _optionLabels[0] = root.Q<VisualElement>("OptionLabel0");
        _optionLabels[1] = root.Q<VisualElement>("OptionLabel1");
    }

    public void PanelOpen(bool isOpen)
    {
        if (isOpen) _panel.AddToClassList("panel--Open");
        else _panel.RemoveFromClassList("panel--Open");
    }

    public void OptionLabelSelect(int index)
    {
        StopAllCoroutines();
        for (int i = 0; i < _optionLabels.Length; i++) 
        {
            _optionLabels[i].RemoveFromClassList("optionLabel--Selected");
            _optionLabels[i].RemoveFromClassList("optionLabel--Selected--Animate");
        }

        
        _optionLabels[index].AddToClassList("optionLabel--Selected");
        StartCoroutine(CInvokeRealtime( () => _optionLabels[index].ToggleInClassList("optionLabel--Selected--Animate") ));
        
    }
    public void OptionLabelUnSelect()
    {
        StopAllCoroutines();
        for (int i = 0; i < _optionLabels.Length; i++)
        {
            _optionLabels[i].RemoveFromClassList("optionLabel--Selected");
            _optionLabels[i].RemoveFromClassList("optionLabel--Selected--Animate");
        }
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }
}
