using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public class S_DialogueManager : Singleton<S_DialogueManager>
{
    [SerializeField] UIDocument UIToolkit;
    [SerializeField] float DISPLAY_TIME;

    private VisualElement _dialoguePanel;
    private Label _dialogueText;
    private ProgressBar _progressBar;

    private List<string> _dialogueTextList = new List<string>();

    private bool _isDisplaying;
    private float _timer;

    private void Start()
    {
        var root = UIToolkit.rootVisualElement;

        _dialoguePanel = root.Q<VisualElement>("DialoguePanel");
        _dialogueText = root.Q<Label>("DialogueText");
        _progressBar = root.Q<ProgressBar>("ProgressBar");

        _progressBar.highValue = DISPLAY_TIME;
        _progressBar.lowValue = 0;
    }
    private void Update()
    {
        if (_dialogueTextList.Count > 0)
        {
            if (!_isDisplaying)
            {
                _isDisplaying = true;
                _timer = 0;

                _dialogueText.text = _dialogueTextList[0];
                _dialoguePanel.AddToClassList("Dialogue--Display");
            }
            else
            {
                _timer += Time.deltaTime;
                if (_timer > DISPLAY_TIME)
                {
                    _dialoguePanel.RemoveFromClassList("Dialogue--Display");
                    if (_timer > DISPLAY_TIME + 1.5f)
                    {
                        _isDisplaying = false;
                        _dialogueTextList.RemoveAt(0);
                    }
                }

                _progressBar.value = _timer;
                _progressBar.title = $"{ String.Format("{0:0.0}", Math.Min( Math.Round(_progressBar.value, 1, MidpointRounding.AwayFromZero), DISPLAY_TIME) ) } s";
            }
        }
    }

    public void DisplayDialogue(string text)
    {
        _dialogueTextList.Add(text);
    }

    [Button]
    public void Dialogue_1()
    {
        DisplayDialogue("Aボタンを押してロボロビ君を起動しよう");
    }
    [Button]
    public void Dialogue_2()
    {
        DisplayDialogue("トゲに当たるとロボロビ君は爆発してしまうぞ");
    }
    [Button]
    public void Dialogue_3()
    {
        DisplayDialogue("Thank you for playing!");
    }
}
