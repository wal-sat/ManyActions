using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectDifficultyUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement[][] _cards = new VisualElement[2][];
    private VisualElement _normalStageIconSleep;
    private VisualElement _normalStageIconSelected;
    private VisualElement _extraStageIconSleep;
    private VisualElement _extraStageIconSelected;
    private VisualElement[] _confirmOptions = new VisualElement[2];
    private VisualElement _confirmPanel;
    private Label _questiontext;

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        for (int i = 0; i < _cards.Length; i++)
        {
            _cards[i] = new VisualElement[2];
        }

        _cards[0][0] = root.Q<VisualElement>("Card_0");
        _cards[1][0] = root.Q<VisualElement>("Card_1");
        _cards[0][1] = root.Q<VisualElement>("Card_2");
        _cards[1][1] = root.Q<VisualElement>("Card_2");

        _normalStageIconSleep = root.Q<VisualElement>("NormalStageIcon_sleep");
        _normalStageIconSelected = root.Q<VisualElement>("NormalStageIcon_selected");
        _extraStageIconSleep = root.Q<VisualElement>("ExtraStageIcon_sleep");
        _extraStageIconSelected = root.Q<VisualElement>("ExtraStageIcon_selected");

        _confirmOptions[0] = root.Q<VisualElement>("ConfirmOptions0");
        _confirmOptions[1] = root.Q<VisualElement>("ConfirmOptions1");

        _confirmPanel = root.Q<VisualElement>("ConfirmPanel");
        _questiontext = root.Q<Label>("QuestionText");
    }

    public void DisplayQuestionText(string str)
    {
        _questiontext.text = str;
    }
    public void OpenOrCloseConfirmPanel(bool open)
    {
        if (open) _confirmPanel.AddToClassList("Panel--Open");
        else _confirmPanel.RemoveFromClassList("Panel--Open");
    }
    public void CardSelect(int x, int y)
    {
        StopAllCoroutines();
        _cards[0][0].RemoveFromClassList("Card--Selected");
        _cards[1][0].RemoveFromClassList("Card--Selected");
        _cards[0][1].RemoveFromClassList("Card-mini--Selected");
        _cards[1][1].RemoveFromClassList("Card-mini--Selected");
        _cards[0][0].RemoveFromClassList("Card--Selected--Animate");
        _cards[1][0].RemoveFromClassList("Card--Selected--Animate");
        _cards[0][1].RemoveFromClassList("Card-mini--Selected--Animate");
        _cards[1][1].RemoveFromClassList("Card-mini--Selected--Animate");

        if (0 <= x && x < _cards.Length)
        {
            if (y == 0)
            {
                _cards[x][y].AddToClassList("Card--Selected");
                StartCoroutine(CInvokeRealtime( () => _cards[x][y].ToggleInClassList("Card--Selected--Animate") ));
            }
            else if (y == 1)
            {
                _cards[x][y].AddToClassList("Card-mini--Selected");
                StartCoroutine(CInvokeRealtime( () => _cards[x][y].ToggleInClassList("Card-mini--Selected--Animate") ));
            }
        }

        _normalStageIconSleep.RemoveFromClassList("StageIcon--off");
        _normalStageIconSelected.AddToClassList("StageIcon--off");
        _extraStageIconSleep.RemoveFromClassList("StageIcon--off");
        _extraStageIconSelected.AddToClassList("StageIcon--off");
        if (x == 0 && y == 0) 
        {
            _normalStageIconSleep.AddToClassList("StageIcon--off");
            _normalStageIconSelected.RemoveFromClassList("StageIcon--off");
        }
        else if (x == 1 && y == 0)
        {
            _extraStageIconSleep.AddToClassList("StageIcon--off");
            _extraStageIconSelected.RemoveFromClassList("StageIcon--off");
        }
    }
    public void CardUnSelect()
    {
        StopAllCoroutines();
        _cards[0][0].RemoveFromClassList("Card--Selected");
        _cards[1][0].RemoveFromClassList("Card--Selected");
        _cards[0][1].RemoveFromClassList("Card-mini--Selected");
        _cards[1][1].RemoveFromClassList("Card-mini--Selected");
        _cards[0][0].RemoveFromClassList("Card--Selected--Animate");
        _cards[1][0].RemoveFromClassList("Card--Selected--Animate");
        _cards[0][1].RemoveFromClassList("Card-mini--Selected--Animate");
        _cards[1][1].RemoveFromClassList("Card-mini--Selected--Animate");
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
    public void ConfirmOptionsUnSelect()
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
}
