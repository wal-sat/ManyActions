using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScenePauseUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _root;

    private VisualElement _confirmPanel;

    private VisualElement[] _menuOptions = new VisualElement[4];
    private VisualElement[] _confirmOptions = new VisualElement[2];

    private void Start()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _root = root.Q<VisualElement>("Root");

        _confirmPanel = root.Q<VisualElement>("ConfirmPanel");

        _menuOptions[0] = root.Q<VisualElement>("MenuOptions0");
        _menuOptions[1] = root.Q<VisualElement>("MenuOptions1");
        _menuOptions[2] = root.Q<VisualElement>("MenuOptions2");
        _menuOptions[3] = root.Q<VisualElement>("MenuOptions3");

        _confirmOptions[0] = root.Q<VisualElement>("ConfirmOptions0");
        _confirmOptions[1] = root.Q<VisualElement>("ConfirmOptions1");
    }

    public void OpenOrCloseConfirmPanel(bool open)
    {
        if (open) _confirmPanel.AddToClassList("Panel--Open");
        else _confirmPanel.RemoveFromClassList("Panel--Open");
    }

    public void MenuOptionsSelect(int index)
    {
        StopAllCoroutines();
        for (int i = 0; i < _menuOptions.Length; i++) 
        {
            _menuOptions[i].RemoveFromClassList("Options--Selected");
            _menuOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }

        if (0 <= index && index < _menuOptions.Length) 
        {
            _menuOptions[index].AddToClassList("Options--Selected");
            StartCoroutine(CInvokeRealtime( () => _menuOptions[index].ToggleInClassList("Options--Selected--Animate") ));
        }
    }
    public void MenuOptionsUnSelected()
    {
        for (int i = 0; i < _menuOptions.Length; i++) 
        {
            StopAllCoroutines();
            _menuOptions[i].RemoveFromClassList("Options--Selected");
            _menuOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }
    }
    public void ConfirmOptionsSelect(int index)
    {
        StopAllCoroutines();
        for (int i = 0; i < _confirmOptions.Length; i++) 
        {
            _confirmOptions[i].RemoveFromClassList("Options--Selected");
            _confirmOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }

        if (0 <= index && index < _confirmOptions.Length) 
        {
            _confirmOptions[index].AddToClassList("Options--Selected");
            StartCoroutine(CInvokeRealtime( () => _confirmOptions[index].ToggleInClassList("Options--Selected--Animate") ));
        }
    }
    public void ConfirmOptionsUnSelected()
    {
        StopAllCoroutines();
        for (int i = 0; i < _confirmOptions.Length; i++) 
        {
            _confirmOptions[i].RemoveFromClassList("Options--Selected");
            _confirmOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }

    public void RootSetActive(bool active)
    {
        _root.style.visibility = Visibility.Hidden;
        if (active) _root.style.visibility = Visibility.Visible;
    }
}
