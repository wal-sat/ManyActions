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

    private VisualElement _actionCard_LR_Swap;
    private VisualElement _actionCard_LR_Kick;
    private VisualElement _actionCard_LR_Interaction;
    private VisualElement _actionCard_UP;
    private VisualElement _actionCard_Front;
    private VisualElement _actionCard_Back;
    private VisualElement _actionCard_Down;
    private VisualElement _actionCard_S_Jump;
    private VisualElement _actionCard_S_DoubleJump;
    private VisualElement _actionCard_S_UP;
    private VisualElement _actionCard_S_Front;
    private VisualElement _actionCard_S_Back;
    private VisualElement _actionCard_S_Down;
    private VisualElement _actionCard_E;
    private VisualElement _actionCard_E_UP;
    private VisualElement _actionCard_E_Front;
    private VisualElement _actionCard_E_Back;
    private VisualElement _actionCard_E_Down;
    private VisualElement _actionCard_W;
    private VisualElement _actionCard_W_UP;
    private VisualElement _actionCard_W_Front;
    private VisualElement _actionCard_W_Back;
    private VisualElement _actionCard_W_Down;
    private VisualElement _actionCard_N;
    private VisualElement _actionCard_N_UP;
    private VisualElement _actionCard_N_Front;
    private VisualElement _actionCard_N_Back;
    private VisualElement _actionCard_N_Down;

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

        _actionCard_LR_Swap = root.Q<VisualElement>("ActionCard_LR_Swap");
        _actionCard_LR_Kick = root.Q<VisualElement>("ActionCard_LR_Kick");
        _actionCard_LR_Interaction = root.Q<VisualElement>("ActionCard_LR_Interaction");
        _actionCard_UP = root.Q<VisualElement>("ActionCard_Up");
        _actionCard_Front = root.Q<VisualElement>("ActionCard_Front");
        _actionCard_Back = root.Q<VisualElement>("ActionCard_Back");
        _actionCard_Down = root.Q<VisualElement>("ActionCard_Down");
        _actionCard_S_Jump = root.Q<VisualElement>("ActionCard_S_Jump");
        _actionCard_S_DoubleJump = root.Q<VisualElement>("ActionCard_S_DoubleJump");
        _actionCard_S_UP = root.Q<VisualElement>("ActionCard_S_UP");
        _actionCard_S_Front = root.Q<VisualElement>("ActionCard_S_Front");
        _actionCard_S_Back = root.Q<VisualElement>("ActionCard_S_Back");
        _actionCard_S_Down = root.Q<VisualElement>("ActionCard_S_Down");
        _actionCard_E = root.Q<VisualElement>("ActionCard_E");
        _actionCard_E_UP = root.Q<VisualElement>("ActionCard_E_Up");
        _actionCard_E_Front = root.Q<VisualElement>("ActionCard_E_Front");
        _actionCard_E_Back = root.Q<VisualElement>("ActionCard_E_Back");
        _actionCard_E_Down = root.Q<VisualElement>("ActionCard_E_Down");
        _actionCard_W = root.Q<VisualElement>("ActionCard_W");
        _actionCard_W_UP = root.Q<VisualElement>("ActionCard_W_Up");
        _actionCard_W_Front = root.Q<VisualElement>("ActionCard_W_Front");
        _actionCard_W_Back = root.Q<VisualElement>("ActionCard_W_Back");
        _actionCard_W_Down = root.Q<VisualElement>("ActionCard_W_Down");
        _actionCard_N = root.Q<VisualElement>("ActionCard_N");
        _actionCard_N_UP = root.Q<VisualElement>("ActionCard_N_Up");
        _actionCard_N_Front = root.Q<VisualElement>("ActionCard_N_Front");
        _actionCard_N_Back = root.Q<VisualElement>("ActionCard_N_Back");
        _actionCard_N_Down = root.Q<VisualElement>("ActionCard_N_Down");
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

    public void VisibleActioncard(Dictionary<ActionKind, bool> availableActions)
    {
        //表示非表示を変える
    }
}
