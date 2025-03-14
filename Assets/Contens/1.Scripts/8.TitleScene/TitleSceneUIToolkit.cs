using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleSceneUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _exitPanel;

    private VisualElement[] _menuOptions = new VisualElement[4];
    private VisualElement[] _exitOptions = new VisualElement[2];

    private void Start()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _exitPanel = root.Q<VisualElement>("Exit");

        _menuOptions[0] = root.Q<VisualElement>("menuOptions0");
        _menuOptions[1] = root.Q<VisualElement>("menuOptions1");
        _menuOptions[2] = root.Q<VisualElement>("menuOptions2");
        _menuOptions[3] = root.Q<VisualElement>("menuOptions3");

        _exitOptions[0] = root.Q<VisualElement>("exitOptions0");
        _exitOptions[1] = root.Q<VisualElement>("exitOptions1");
    }

    public void OpenOrCloseExitPanel(bool open)
    {
        if (open) _exitPanel.AddToClassList("Panel--Open");
        else _exitPanel.RemoveFromClassList("Panel--Open");
    }

    private int _selectedMenuOptionIndex;
    private int _selectedExitOptionIndex;
    public void MenuOptionsSelect(int index)
    {
        for (int i = 0; i < _menuOptions.Length; i++) 
        {
            CancelInvoke("AnimateMenuOptions");
            _menuOptions[i].RemoveFromClassList("Options--Selected");
            _menuOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }

        if (0 <= index && index < _menuOptions.Length) 
        {
            _menuOptions[index].AddToClassList("Options--Selected");
            _selectedMenuOptionIndex = index;
            InvokeRepeating( "AnimateMenuOptions", 0.1f, 1f);
        }
        else CancelInvoke("AnimateMenuOptions");
    }
    public void MenuOptionsUnSelected()
    {
        for (int i = 0; i < _menuOptions.Length; i++) 
        {
            CancelInvoke("AnimateMenuOptions");
            _menuOptions[i].RemoveFromClassList("Options--Selected");
            _menuOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }
    }
    public void ExitOptionsSelect(int index)
    {
        for (int i = 0; i < _exitOptions.Length; i++) 
        {
            CancelInvoke("AnimateExitOptions");
            _exitOptions[i].RemoveFromClassList("exitOptions--Selected");
            _exitOptions[i].RemoveFromClassList("exitOptions--Selected--Animate");
        }

        if (0 <= index && index < _exitOptions.Length)
        {
            _exitOptions[index].AddToClassList("exitOptions--Selected");
            _selectedExitOptionIndex = index;
            InvokeRepeating("AnimateExitOptions", 0.1f, 1f);
        }
        else CancelInvoke("AnimateExitOptions");
    }
    public void ExitOptionsUnSelect()
    {
        for (int i = 0; i < _exitOptions.Length; i++) 
        {
            CancelInvoke("AnimateExitOptions");
            _exitOptions[i].RemoveFromClassList("exitOptions--Selected");
            _exitOptions[i].RemoveFromClassList("exitOptions--Selected--Animate");
        }
    }

    private void AnimateMenuOptions()
    {
        _menuOptions[_selectedMenuOptionIndex].ToggleInClassList("Options--Selected--Animate");
    }
    private void AnimateExitOptions()
    {
        _exitOptions[_selectedExitOptionIndex].ToggleInClassList("exitOptions--Selected--Animate");
    }
}
