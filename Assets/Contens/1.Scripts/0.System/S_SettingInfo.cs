using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_SettingInfo : Singleton<S_SettingInfo>
{
    private int _settingIndex = 0;
    int settingIndex
    {
        get => _settingIndex;
        set
        {
            _settingIndex = Mathf.Clamp(value, 0, 3);

            SettingOptionsSelect(_settingIndex);
        }
    }
    private int _masterVolume = 8;
    int masterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = Mathf.Clamp(value, 0, 10);
            ChangeBarValueMaster(_masterVolume);
            S_BGMManager._instance.ChangeVolume( (float) _masterVolume * _BGMVolume / 100);
            S_SEManager._instance.ChangeVolume( (float) _masterVolume * _SEVolume / 100);
        }
    }
    private int _BGMVolume = 8;
    int BGMVolume
    {
        get => _BGMVolume;
        set
        {
            _BGMVolume = Mathf.Clamp(value, 0, 10);
            ChangeBarValueBGM(_BGMVolume);
            S_BGMManager._instance.ChangeVolume( (float) _masterVolume * _BGMVolume / 100);
        }
    }
    private int _SEVolume = 8;
    int SEVolume
    {
        get => _SEVolume;
        set
        {
            _SEVolume = Mathf.Clamp(value, 0, 10);
            ChangeBarValueSE(_SEVolume);
            S_SEManager._instance.ChangeVolume( (float) _masterVolume * _SEVolume / 100);
        }
    }

    private void Start()
    {
        settingIndex = 0;
        masterVolume = 8;
        BGMVolume = 8;
        SEVolume = 8;   
    }

    public void CursorUp()
    {
        settingIndex --;
    }
    public void CursorDown()
    {
        settingIndex ++;
    }
    public void CursorRight()
    {
        if (settingIndex == 0) masterVolume ++;
        else if (settingIndex == 1) BGMVolume ++;
        else if (settingIndex == 2) SEVolume ++;
    }
    public void CursorLeft()
    {
        if (settingIndex == 0) masterVolume --;
        else if (settingIndex == 1) BGMVolume --;
        else if (settingIndex == 2) SEVolume --;
    }
    public bool CursorSelect()
    {
        if (settingIndex == 3) 
        {
            settingIndex = 0;
            OpenOrCloseSettingPanel(false);
            return true;
        }
        return false;
    }
    public void CursorCancel()
    {
        settingIndex = 0;
        OpenOrCloseSettingPanel(false);
    }

    //-----UIToolkit-----
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _settingPanel;

    private VisualElement[] _settingOptions = new VisualElement[4];
    private VisualElement[] _barValue = new VisualElement[3];

    public override void Awake()
    {
        base.Awake();

        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _settingPanel = root.Q<VisualElement>("Setting");

        _settingOptions[0] = root.Q<VisualElement>("settingOptions0");
        _settingOptions[1] = root.Q<VisualElement>("settingOptions1");
        _settingOptions[2] = root.Q<VisualElement>("settingOptions2");
        _settingOptions[3] = root.Q<VisualElement>("settingOptions3");

        _barValue[0] = root.Q<VisualElement>("BarValue-master");
        _barValue[1] = root.Q<VisualElement>("BarValue-BGM");
        _barValue[2] = root.Q<VisualElement>("BarValue-SE");
    }

    public void OpenOrCloseSettingPanel(bool isOpen)
    {
        if (isOpen) _settingPanel.AddToClassList("settingPanel--Open");
        else _settingPanel.RemoveFromClassList("settingPanel--Open");
    }

    public void SettingOptionsSelect(int index)
    {
        for (int i = 0; i < _settingOptions.Length; i++) 
        {
            StopAllCoroutines();
            _settingOptions[i].RemoveFromClassList("settingOptions--Selected");
            _settingOptions[i].RemoveFromClassList("settingOptions--Selected--Animate");
        }

        if (0 <= index && index < _settingOptions.Length) 
        {
            _settingOptions[index].AddToClassList("settingOptions--Selected");
            StartCoroutine(CInvokeRealtime( () => _settingOptions[index].ToggleInClassList("settingOptions--Selected--Animate") ));
        }
        else StopAllCoroutines();
    }
    public void SettingOptionsUnSelected()
    {
        for (int i = 0; i < _settingOptions.Length; i++) 
        {
            StopAllCoroutines();
            _settingOptions[i].RemoveFromClassList("Options--Selected");
            _settingOptions[i].RemoveFromClassList("Options--Selected--Animate");
        }
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }

    private void ChangeBarValueMaster(int volume)
    {
        _barValue[0].style.width = Length.Percent(volume * 10);
    }
    private void ChangeBarValueBGM(int volume)
    {
        _barValue[1].style.width = Length.Percent(volume * 10);
    }
    private void ChangeBarValueSE(int volume)
    {
        _barValue[2].style.width = Length.Percent(volume * 10);
    }
}
