using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class GameSceneClearUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;
    [SerializeField] private Sprite gearIcon;
    [SerializeField] private Sprite gearDisableIcon;

    private VisualElement _root;
    private VisualElement _clearIcon;
    private VisualElement[] _optionLabel = new VisualElement[2];
    private VisualElement[] _gearIcon = new VisualElement[5];

    private Label _messageLabel;
    private Label _deathCountLabel;
    private Label _minimumDeathCountLabel;
    private Label _clearTimeLabel;
    private Label _fastestClearTimeLabel;

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _root = root.Q<VisualElement>("Root");
        _clearIcon = root.Q<VisualElement>("ClearIcon");
        for (int i = 0; i < _optionLabel.Length; i++) _optionLabel[i] = root.Q<VisualElement>("OptionLabel" + i);
        for (int i = 0; i < _gearIcon.Length; i++) _gearIcon[i] = root.Q<VisualElement>("GearIcon" + i);

        _messageLabel = root.Q<Label>("MessageLabel");
        _deathCountLabel =  root.Q<Label>("DeathCountLabel");
        _minimumDeathCountLabel =  root.Q<Label>("MinimumDeathCountLabel");
        _clearTimeLabel =  root.Q<Label>("ClearTimeLabel");
        _fastestClearTimeLabel =  root.Q<Label>("FastestClearTimeLabel");
    }

    public void RootVisible(bool isVisible)
    {
        if (isVisible) _root.RemoveFromClassList("Invisible");
        else _root.AddToClassList("Invisible");
    }

    /// <summary>
    /// クリアアイコンの表示を変更する
    /// </summary>
    public void ChangeClearIcon(Sprite sprite)
    {
        if (_clearIcon == null) return;
        _clearIcon.style.backgroundImage = sprite.texture;
    }
  

    /// <summary>
    /// オプションラベルの選択状態を変更する
    /// </summary>
    public void OptionLabelSelect(int index)
    {
        StopAllCoroutines();
        for (int i = 0; i < _optionLabel.Length; i++) 
        {
            _optionLabel[i].RemoveFromClassList("optionLabel--Selected");
            _optionLabel[i].RemoveFromClassList("optionLabel--Selected--Animate");
        }
 
        _optionLabel[index].AddToClassList("optionLabel--Selected");
        StartCoroutine(CInvokeRealtime( () => _optionLabel[index].ToggleInClassList("optionLabel--Selected--Animate") ));
        
    }

    /// <summary>
    /// 歯車のアイコンの取得状態を変更する
    /// </summary>
    public void GearIconAcquired(int index, bool isAcquired)
    {
        if (_gearIcon[index] == null) return;
        if (isAcquired) _gearIcon[index].style.backgroundImage = gearIcon.texture;
        else _gearIcon[index].style.backgroundImage = gearDisableIcon.texture;
    }

    /// <summary>
    /// メッセージラベルの表示を変更する
    /// </summary>
    public void ChangeMessageLabel(string message)
    {
        _messageLabel.text = "～" + message + "～";
    }

    /// <summary>
    /// デス数の表示を変更する
    /// </summary>
    public void ChangeDeathCountLabel(int deathCount)
    {
        _deathCountLabel.text = "デス数 ： " + deathCount.ToString();
    }

    /// <summary>
    /// 最小デス数の表示を変更する
    /// </summary>
    public void ChangeMinimumDeathCountLabel(int minimumDeathCount)
    {
        _minimumDeathCountLabel.text = "最小デス数 ： " + minimumDeathCount.ToString();
    }

    /// <summary>
    /// プレイ時間の表示を変更する
    /// </summary>
    /// <param name="playTime">プレイ時間</param>
    public void ChangeClearTimeLabel(string clearTime)
    {
        _clearTimeLabel.text = "プレイ時間 ： " + clearTime;
    }

    /// <summary>
    /// 最速クリアタイムの表示を変更する
    /// </summary>
    public void ChangeFastestClearTimeLabel(string fastestClearTime)
    {
        _fastestClearTimeLabel.text = "最速クリア時間 ： " + fastestClearTime;
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }
}
