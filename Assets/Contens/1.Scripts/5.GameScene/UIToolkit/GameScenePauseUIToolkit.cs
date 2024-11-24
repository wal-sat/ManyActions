using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScenePauseUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _root;

    private VisualElement _settingPanel;
    private VisualElement _confirmPanel;

    private VisualElement[] _menuOptions = new VisualElement[3];
    private VisualElement[] _confirmOptions = new VisualElement[2];

    private void Start()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _root = root.Q<VisualElement>("Root");

        _settingPanel = root.Q<VisualElement>("SettingPanel");
        _confirmPanel = root.Q<VisualElement>("ConfirmPanel");

        _menuOptions[0] = root.Q<VisualElement>("MenuOptions0");
        _menuOptions[1] = root.Q<VisualElement>("MenuOptions1");
        _menuOptions[2] = root.Q<VisualElement>("MenuOptions2");

        _confirmOptions[0] = root.Q<VisualElement>("ConfirmOptions0");
        _confirmOptions[1] = root.Q<VisualElement>("ConfirmOptions1");
    }

    public void OpenOrCloseSettingPanel(bool open)
    {
        if (open) _settingPanel.AddToClassList("Panel--Open");
        else _settingPanel.RemoveFromClassList("Panel--Open");
    }
    public void OpenOrCloseConfirmPanel(bool open)
    {
        if (open) _confirmPanel.AddToClassList("Panel--Open");
        else _confirmPanel.RemoveFromClassList("Panel--Open");
    }

    public void MenuOptionsSelect(int index)
    {
        for (int i = 0; i < _menuOptions.Length; i++) _menuOptions[i].RemoveFromClassList("Options--Selected");

        if (0 <= index && index < _menuOptions.Length) _menuOptions[index].AddToClassList("Options--Selected");
    }
    public void ConfirmOptionsSelect(int index)
    {
        for (int i = 0; i < _confirmOptions.Length; i++) _confirmOptions[i].RemoveFromClassList("Options--Selected");

        if (0 <= index && index < _confirmOptions.Length) _confirmOptions[index].AddToClassList("Options--Selected");
    }

    public void RootSetActive(bool active)
    {
        _root.style.visibility = Visibility.Hidden;
        if (active) _root.style.visibility = Visibility.Visible;
    }
}
