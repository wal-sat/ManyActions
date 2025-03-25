using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class StageSelectUIToolkit : MonoBehaviour
{
    [SerializeField] private GameObject UIToolkit;
    [SerializeField] private Sprite gearIcon;
    [SerializeField] private Sprite gearDisableIcon;

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
    private VisualElement _toUndergroundStageIcon;
    private VisualElement _toReverseStageIcon;

    private Label[,,] _stageNameLabel = new Label[5, 2, 2];
    private Label[,,] _minimumDeathCountLabel = new Label[5, 2, 2];
    private Label[,,] _deathCountLabel = new Label[5, 2, 2];
    private Label[,,] _FastestClearTimeLabel = new Label[5, 2, 2];
    private Label[,,] _playTimeLabel = new Label[5, 2, 2];

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
                    if (_stage[i, j, k] == null)
                    {
                        Debug.Log("Stage" + i + j + k + " is null");
                        continue;
                    }
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
                    _deathCountLabel[i, j, k] = _stage[i, j, k].Q<Label>("DeathCountLabel");
                    _FastestClearTimeLabel[i, j, k] = _stage[i, j, k].Q<Label>("FastestClearTimeLabel");
                    _playTimeLabel[i, j, k] = _stage[i, j, k].Q<Label>("PlayTimeLabel");
                }
            }
        }

        _toUndergroundStageIcon = root.Q<VisualElement>("ToUndergroundStageIcon");
        _toReverseStageIcon = root.Q<VisualElement>("ToReverseStageIcon");
    }

    /// <summary>
    /// ステージパネルを移動する
    /// </summary>
    public void StagePanelMove(int i, int j)
    {
        _stagePanel.style.translate = new Translate(Length.Percent(-100*i), Length.Percent(-100*j));
        _reverseStagePanel.style.translate = new Translate(Length.Percent(-100*i), Length.Percent(-100*j));
    }
    public void StagePanelReverse(int k)
    {
        S_FadeManager._instance.Fade(() => StagePanelVisibilitySwitch(k), () => {}, FadeType.Black, 0.4f, 0.3f, 0.4f);
    }
    public void StagePanelVisibilitySwitch(int k)
    {
        if (k == 0) _stagePanel.style.visibility= Visibility.Visible;
        else _stagePanel.style.visibility = Visibility.Hidden;
        if (k == 1) _reverseStagePanel.style.visibility= Visibility.Visible;
        else _reverseStagePanel.style.visibility = Visibility.Hidden;
    }


    /// <summary>
    /// ステージラベルの選択状態を変更する
    /// </summary>
    public void StageLabelSelect(int index, int i, int j, int k)
    {
        StopAllCoroutines();
        for (int l = 0; l < 5; l++) 
        {
            if (_stageLabel[l,i,j,k] == null) 
            {
                Debug.Log("StageLabel" + l + i + j + k + " is null");
                continue;
            }
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected");
            _stageLabel[l,i,j,k].RemoveFromClassList("stageLabel--Selected--Animate");
        }

        
        _stageLabel[index,i,j,k].AddToClassList("stageLabel--Selected");
        StartCoroutine(CInvokeRealtime( () => _stageLabel[index,i,j,k].ToggleInClassList("stageLabel--Selected--Animate") ));
        
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

    /// <summary>
    /// 初期アクションの画像を変更する
    /// </summary>
    /// <param name="acquireActionIcon">表示する初期アクションのスプライト</param>
    public void AcquireActionImageChange(int i, int j, int k, Sprite acquireActionIcon)
    {
        if (_acquireActionIcon[i,j,k] == null) return;
        _acquireActionIcon[i,j,k].style.backgroundImage = acquireActionIcon.texture;
    }

    /// <summary>
    /// ステージ名の表示を変更する
    /// </summary>
    /// <param name="stageName">表示するステージ名</param>
    public void StageNameLabelChange(int i, int j, int k, string worldName, string stageName)
    {
        if (_stageNameLabel[i,j,k] == null) return;
        _stageNameLabel[i,j,k].text = worldName + " " + stageName;
    }

    /// <summary>
    /// ステージイメージの表示を変更する
    /// </summary>
    /// <param name="stageImage">表示するステージイメージのスプライト</param>
    public void StageImageChange(int i, int j, int k, Sprite stageImage)
    {
        if (_stageImage[i,j,k] == null) return;
        _stageImage[i,j,k].style.backgroundImage = stageImage.texture;
    }

    /// <summary>
    /// 歯車のアイコンの取得状態を変更する
    /// </summary>
    /// <param name="isAcquired">trueなら取得、falseなら未取得</param>
    public void GearIconAcquired(int index, int i, int j, int k, bool isAcquired)
    {
        if (_gearIcon[index, i, j, k] == null) return;
        if (isAcquired) _gearIcon[index, i, j, k].style.backgroundImage = gearIcon.texture;
        else _gearIcon[index, i, j, k].style.backgroundImage = gearDisableIcon.texture;
    }

    /// <summary>
    /// 最小デス数の表示を変更する
    /// </summary>
    /// <param name="minimumDeathCount">最小デス数</param>
    public void MinimumDeathCountLabelChange(int i, int j, int k, int minimumDeathCount)
    {
        if (_minimumDeathCountLabel[i,j,k] == null) return;
        if (minimumDeathCount == -1) _minimumDeathCountLabel[i,j,k].text = "最小デス数 ： ---";
        else _minimumDeathCountLabel[i,j,k].text = "最小デス ： " + minimumDeathCount.ToString();
    }

    /// <summary>
    /// 合計デス数の表示を変更する
    /// </summary>
    /// <param name="deathCount">合計デス数</param>
    public void DeathCountLabelChange(int i, int j, int k, int deathCount)
    {
        if (_deathCountLabel[i,j,k] == null) return;
        _deathCountLabel[i,j,k].text = "合計デス ： " + deathCount.ToString();
    }

    /// <summary>
    /// 最速クリアタイムの表示を変更する
    /// </summary>
    /// <param name="fastestClearTime">最速クリア時間</param>
    public void FastestClearTimeLabelChange(int i, int j, int k, int fastestClearTime)
    {
        if (_FastestClearTimeLabel[i,j,k] == null) return;
        if (fastestClearTime == -1) _FastestClearTimeLabel[i,j,k].text = "最速クリア時間 ： ---";
        else
        {
            int[] ints = new int[3];
            ints[0] = fastestClearTime / 3600;
            ints[1] = (fastestClearTime % 3600) / 60;
            ints[2] = fastestClearTime % 60;
            _FastestClearTimeLabel[i,j,k].text = $"最速クリア時間 ： {ints[0]}:{ints[1]}:{ints[2]}";
        }
    }

    /// <summary>
    /// プレイ時間の表示を変更する
    /// </summary>
    /// <param name="playTime">プレイ時間</param>
    public void PlayTimeLabelChange(int i, int j, int k, int playTime)
    {
        if (_playTimeLabel[i,j,k] == null) return;
        if (playTime == -1) _playTimeLabel[i,j,k].text = "プレイ時間 ： ---";
        else 
        {
            int[] ints = new int[3];
            ints[0] = playTime / 3600;
            ints[1] = (playTime % 3600) / 60;
            ints[2] = playTime % 60;
            _playTimeLabel[i,j,k].text = $"プレイ時間 ： {ints[0]}:{ints[1]}:{ints[2]}";
        }
    }

    /// <summary>
    /// 矢印の表示を変更する
    /// </summary>
    /// <param name="isVisible">trueなら表示、falseなら非表示</param>
    public void LeftArrowVisibility(int i, int j, int k, bool isVisible)
    {
        if (_leftArrow[i,j,k] == null) return;
        if (isVisible) _leftArrow[i,j,k].style.display = DisplayStyle.Flex;
        else _leftArrow[i,j,k].style.display = DisplayStyle.None;
    }
    public void RightArrowVisibility(int i, int j, int k, bool isVisible)
    {
        if (_rightArrow[i,j,k] == null) return;
        if (isVisible) _rightArrow[i,j,k].style.display = DisplayStyle.Flex;
        else _rightArrow[i,j,k].style.display = DisplayStyle.None;
    }

    /// <summary>
    /// 地下ステージアイコンの表示を変更する
    /// </summary>
    /// <param name="isDisplay">trueなら表示、falseなら非表示</param>
    public void ToUndergroundStageIconDisplay(bool isDisplay)
    {
        if (isDisplay) _toUndergroundStageIcon.style.display = DisplayStyle.Flex;
        else _toUndergroundStageIcon.style.display = DisplayStyle.None;
    }
    /// <summary>
    /// 裏ステージアイコンの表示を変更する
    /// </summary>
    /// <param name="isDisplay">trueなら表示、falseなら非表示</param>
    public void ToReverseStageIconDisplay(bool isDisplay)
    {
        if (isDisplay) _toReverseStageIcon.style.display = DisplayStyle.Flex;
        else _toReverseStageIcon.style.display = DisplayStyle.None;
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }
}
