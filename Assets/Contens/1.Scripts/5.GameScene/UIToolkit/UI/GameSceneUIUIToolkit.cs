using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using NaughtyAttributes;
using System;

public class GameSceneUIUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _root;
    private VisualElement _kidouUI;
    private VisualElement _sleepCameraUI;
    private VisualElement _actionCard;
    private VisualElement _actionIcon;

    private Label _actionTitleLabel;
    private Label _actionNameLabel;
    private Label _gearLabel;
    private Label _gearPlusLabel;
    private Label _deathLabel;
    private Label _stageNameLabel;
    private Label _timeLabel;

    private bool _isProcessing = false;
    private Queue<IEnumerator> makeActionCardQueue = new Queue<IEnumerator>();

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _root = root.Q<VisualElement>("Root");
        _kidouUI = root.Q<VisualElement>("KidouUI");
        _sleepCameraUI = root.Q<VisualElement>("SleepCameraUI");
        _actionCard = root.Q<VisualElement>("ActionCard");
        _actionIcon = root.Q<VisualElement>("ActionIcon");

        _actionTitleLabel = root.Q<Label>("ActionTitleLabel");
        _actionNameLabel = root.Q<Label>("ActionNameLabel");
        _gearLabel = root.Q<Label>("GearLabel");
        _gearPlusLabel = root.Q<Label>("GearPlusLabel");
        _deathLabel = root.Q<Label>("DeathLabel");
        _stageNameLabel = root.Q<Label>("StageNameLabel");
        _timeLabel = root.Q<Label>("TimeLabel");
    }

    public void MakeActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        makeActionCardQueue.Enqueue( CActionCard(isAcquired, actionName, actionIcon) );
        if (!_isProcessing) StartCoroutine( ProcessQueue() );
    }
    private IEnumerator ProcessQueue()
    {
        while (makeActionCardQueue.Count > 0)
        {
            _isProcessing = true;
            yield return StartCoroutine(makeActionCardQueue.Dequeue());
        }
        _isProcessing = false;
    }
    IEnumerator CActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        if (isAcquired) _actionTitleLabel.text = "アクションゲット";
        else _actionTitleLabel.text = "アクションロスト";

        _actionNameLabel.text = actionName;
        _actionIcon.style.backgroundImage = new StyleBackground(actionIcon);

        yield return new WaitForSeconds(0.1f);

        _actionCard.AddToClassList("ActionCard--On");

        yield return new WaitForSeconds(4.0f);

        _actionCard.RemoveFromClassList("ActionCard--On");

        yield return new WaitForSeconds(0.5f);
    }

    public void ChangeGearLabel(int count)
    {
        string addText = "";
        if (count < 10) addText += "0";
        _gearLabel.text = addText + count.ToString();
    }
    public void ChangeGearPlusLabel(int count)
    {
        _gearPlusLabel.text = "(+" + count.ToString() + ")";
    }
    public void ChangeDeathLabel(int count)
    {
        string addText = "";
        if (count < 10) addText += "0";
        _deathLabel.text = addText + count.ToString();
    }
    public void ChangeStageNameLabel(string worldName, string stageName)
    {
        _stageNameLabel.text = worldName + "\n" + stageName;
    }
    public void ChangeTimeLabel(string timeString)
    {
        _timeLabel.text = timeString;
    }

    public void SetActiveGearPlusLabel(bool isActive)
    {
        _gearPlusLabel.style.visibility = Visibility.Hidden;
        if (isActive) _gearPlusLabel.style.visibility = Visibility.Visible;
    }

    public void RootFade(bool isVisible)
    {
        if (isVisible) _root.RemoveFromClassList("Unvisible");
        else if (!isVisible) _root.AddToClassList("Unvisible");
    }
    public void KidouUIFade(bool isVisible)
    {
        if (isVisible) _kidouUI.RemoveFromClassList("Unvisible");
        else if (!isVisible) _kidouUI.AddToClassList("Unvisible");
    }
    public void SleepCameraUIFade(bool isVisible)
    {
        if (isVisible) _sleepCameraUI.RemoveFromClassList("Unvisible");
        else if (!isVisible) _sleepCameraUI.AddToClassList("Unvisible");
    }
}
