using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    public void Initialize()
    {
        if (buttons == null) return;
        foreach (var button in buttons)
        {
            button.Init();
        }
    }
}
