using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleSceneUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;
    private VisualElement[] _titleOptions = new VisualElement[3];

    private void Start()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _titleOptions[0] = root.Q<VisualElement>("titleOptions0");
        _titleOptions[1] = root.Q<VisualElement>("titleOptions1");
        _titleOptions[2] = root.Q<VisualElement>("titleOptions2");
    }

    public void MenuOptionsSelect(int index)
    {
        for (int i = 0; i < _titleOptions.Length; i++) OptionUnselected(_titleOptions[i]);

        if (0 <= index && index < _titleOptions.Length) OptionSelected(_titleOptions[index]);
    }

    private void OptionSelected(VisualElement option)
    {
        option.AddToClassList("Options--Selected");
    }
    private void OptionUnselected(VisualElement option)
    {
        option.RemoveFromClassList("Options--Selected");
    }

    [Button]
    public void zero()
    {
        MenuOptionsSelect(0);
    }
    [Button]
    public void one()
    {
        MenuOptionsSelect(1);
    }
}
