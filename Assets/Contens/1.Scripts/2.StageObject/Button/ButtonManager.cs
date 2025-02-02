using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private List<Button> _buttons = new List<Button>();

    public void Register(Button button)
    {
        _buttons.Add(button);
    }

    public void Initialize()
    {
        if (_buttons == null) return;
        foreach (var button in _buttons)
        {
            button.Init();
        }
    }
}
