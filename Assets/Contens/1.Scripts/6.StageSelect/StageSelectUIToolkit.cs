using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using NaughtyAttributes;

public class StageSelectUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;

    private VisualElement _stagePanel;
    private VisualElement _reverseStagePanel;
    private VisualElement[,,] _stage = new VisualElement[5, 2, 2];
    private VisualElement[,,,] _stageLabel = new VisualElement[5, 5, 2, 2];
    private VisualElement[,,,] _padlock = new VisualElement[5, 5, 2, 2];
    private VisualElement[,,] _acquireActionIcon = new VisualElement[5, 2, 2];
    private VisualElement[,,] _stageImage = new VisualElement[5, 2, 2];
    private VisualElement[,,,] _gearIcon = new VisualElement[5, 5, 2, 2];
    private VisualElement[,,] _leftArrow = new VisualElement[5, 2, 2];
    private VisualElement[,,] _rightArrow = new VisualElement[5, 2, 2];

    private Label[,,] _stageNameLabel = new Label[5, 2, 2];
    private Label[,,] _minimumDeathCountLabel = new Label[5, 2, 2];
    private Label[,,] _FastestClearTimeLabel = new Label[5, 2, 2];

    private void Awake()
    {
        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _stagePanel = root.Q<VisualElement>("StagePanel");
        _reverseStagePanel = root.Q<VisualElement>("ReverseStagePanel");

        _stage[0, 0, 0] = _stagePanel.Q<VisualElement>("Plain");
        _stage[1, 0, 0] = _stagePanel.Q<VisualElement>("Blue");
        _stage[2, 0, 0] = _stagePanel.Q<VisualElement>("Green");
        _stage[3, 0, 0] = _stagePanel.Q<VisualElement>("Yellow");
        _stage[4, 0, 0] = _stagePanel.Q<VisualElement>("Purple");
        _stage[0, 1, 0] = _stagePanel.Q<VisualElement>("UndergroundPlain");
        _stage[1, 1, 0] = _stagePanel.Q<VisualElement>("UndergroundBlue");
        _stage[2, 1, 0] = _stagePanel.Q<VisualElement>("UndergroundGreen");
        _stage[3, 1, 0] = _stagePanel.Q<VisualElement>("UndergroundYellow");
        _stage[4, 1, 0] = _stagePanel.Q<VisualElement>("UndergroundPurple");
        _stage[0, 0, 1] = _reverseStagePanel.Q<VisualElement>("Plain");
        _stage[1, 0, 1] = _reverseStagePanel.Q<VisualElement>("Blue");
        _stage[2, 0, 1] = _reverseStagePanel.Q<VisualElement>("Green");
        _stage[3, 0, 1] = _reverseStagePanel.Q<VisualElement>("Yellow");
        _stage[4, 0, 1] = _reverseStagePanel.Q<VisualElement>("Purple");
        _stage[0, 1, 1] = _reverseStagePanel.Q<VisualElement>("UndergroundPlain");
        _stage[1, 1, 1] = _reverseStagePanel.Q<VisualElement>("UndergroundBlue");
        _stage[2, 1, 1] = _reverseStagePanel.Q<VisualElement>("UndergroundGreen");
        _stage[3, 1, 1] = _reverseStagePanel.Q<VisualElement>("UndergroundYellow");
        _stage[4, 1, 1] = _reverseStagePanel.Q<VisualElement>("UndergroundPurple");

        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 2; j++) {
                for (int k = 0; k < 2; k++) {
                    if (_stage[i, j, k] == null) continue;
                    for (int l = 0; l < 5; l++)
                    {
                        _stageLabel[l, i, j, k] = _stage[i, j, k].Q<VisualElement>("StageLabel" + l);
                        _padlock[l, i, j, k] = _stage[i, j, k].Q<VisualElement>("Padlock" + l);
                        _gearIcon[l, i, j, k] = _stage[i, j, k].Q<VisualElement>("GearIcon" + l);
                        
                    }
                    _acquireActionIcon[i, j, k] = _stage[i, j, k].Q<VisualElement>("AcquireActionIcon");
                    _stageImage[i, j, k] = _stage[i, j, k].Q<VisualElement>("StageImage");
                    _leftArrow[i, j, k] = _stage[i, j, k].Q<VisualElement>("LeftArrow");
                    _rightArrow[i, j, k] = _stage[i, j, k].Q<VisualElement>("RightArrow");
                    _stageNameLabel[i, j, k] = _stage[i, j, k].Q<Label>("StageNameLabel");
                    _minimumDeathCountLabel[i, j, k] = _stage[i, j, k].Q<Label>("MinimumDeathCountLabel");
                    _FastestClearTimeLabel[i, j, k] = _stage[i, j, k].Q<Label>("FastestClearTimeLabel");
                }
            }
        }
    }

    /// <summary>
    /// ステージラベルの選択状態を変更する
    /// </summary>
    public void StageLabelSelect(int index, int i, int j, int k)
    {
        StopAllCoroutines();
        for (int l = 0; l < 5; l++) 
        {
            if (_stageLabel[l,i,j,k] == null) continue;
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected");
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected--Animate");
        }

        if (0 <= index && index < 5) 
        {
            _stageLabel[index,i,j,k].AddToClassList("stageLabel--Selected");
            StartCoroutine(CInvokeRealtime( () => _stageLabel[index,i,j,k].ToggleInClassList("stageLabel--Selected--Animate") ));
        }
    }
    public void StageLabelUnSelect(int i, int j, int k)
    {
        StopAllCoroutines();
        for (int l = 0; l < 5; l++) 
        {
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected");
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected--Animate");
        }
    }

    /// <summary>
    /// 南京錠の表示を変更する
    /// </summary>
    /// <param name="isVisible">trueなら表示、falseなら非表示</param>
    public void PadlockVisibility(int index, int i, int j, int k, bool isVisible)
    {
        if (_padlock[index,i,j,k] == null) return;
        if (isVisible) _padlock[index,i,j,k].style.display = DisplayStyle.Flex;
        else _padlock[index,i,j,k].style.display = DisplayStyle.None;
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }

    [Button]
    public void Test()
    {
        PadlockVisibility(0, 0, 0, 0, true);
        PadlockVisibility(1, 0, 0, 0, false);
    }
}
