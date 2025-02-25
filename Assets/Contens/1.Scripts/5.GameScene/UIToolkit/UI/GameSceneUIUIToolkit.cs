using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using NaughtyAttributes;

public class GameSceneUIUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;
    [SerializeField] VisualTreeAsset ActionCardTemplate;

    private VisualElement _root;
    private VisualElement _kidouUI;
    private VisualElement _sleepCameraUI;
    private VisualElement _actionCardInventory;

    private Label _gearLabel;
    private Label _gearPlusLabel;
    private Label _deathLabel;
    private Label _timeLabel;

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _root = root.Q<VisualElement>("Root");
        _kidouUI = root.Q<VisualElement>("KidouUI");
        _sleepCameraUI = root.Q<VisualElement>("SleepCameraUI");
        _actionCardInventory = root.Q<VisualElement>("ActionCardInventory");

        _gearLabel = root.Q<Label>("GearLabel");
        _gearPlusLabel = root.Q<Label>("GearPlusLabel");
        _deathLabel = root.Q<Label>("DeathLabel");
        _timeLabel = root.Q<Label>("TimeLabel");
    }

    public void MakeActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        StartCoroutine( CActionCard(isAcquired, actionName, actionIcon) );
    }

    IEnumerator CActionCard(bool isAcquired, string actionName, Sprite actionIcon)
    {
        TemplateContainer templateContainer = ActionCardTemplate.Instantiate();
        _actionCardInventory.Add(templateContainer);

        VisualElement actionCard = templateContainer.Q<VisualElement>("ActionCard");
        VisualElement _actionIcon = actionCard.Q<VisualElement>("ActionIcon");
        Label _titleLabel = actionCard.Q<Label>("TitleLabel");
        Label _actionLabel = actionCard.Q<Label>("ActionLabel");

        if (isAcquired) _titleLabel.text = "習\n\n得";
        else _titleLabel.text = "喪\n\n失";

        _actionLabel.text = actionName;
        _actionIcon.style.backgroundImage = new StyleBackground(actionIcon);

        yield return new WaitForSeconds(0.1f);

        actionCard.AddToClassList("ActionCard--on");

        yield return new WaitForSeconds(5.0f);

        actionCard.RemoveFromClassList("ActionCard--on");

        yield return new WaitForSeconds(0.6f);

        actionCard.AddToClassList("ActionCard--off");

        yield return new WaitForSeconds(1f);

        _actionCardInventory.Remove(templateContainer);
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
    public void ChangeTimeLabel(int[] times)
    {
        string addMiniteText = "";
        string addSecondText = "";
        if (times[0] < 10) addMiniteText += "0";
        if (times[1] < 10) addSecondText += "0";
        _timeLabel.text = addMiniteText + times[0].ToString() + ":" + addSecondText + times[1].ToString();
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
