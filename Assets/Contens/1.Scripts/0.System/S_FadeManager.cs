using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public enum FadeType { Black, White, Diamond }

public class S_FadeManager : Singleton<S_FadeManager>
{
    [SerializeField] GameObject UIToolkit;

    private VisualElement _blackPanel;
    private VisualElement _whitePanel;
    private VisualElement[] _diamondPanel = new VisualElement[2];

    public override void Awake()
    {
        base.Awake();

        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        _blackPanel = root.Q<VisualElement>("Black");
        _whitePanel = root.Q<VisualElement>("White");
        _diamondPanel[0] = root.Q<VisualElement>("Diamond");
        _diamondPanel[1] = root.Q<VisualElement>("Diamond_White");
    }

    public void Fade(Action inAction, Action endAction, FadeType fadeType = FadeType.Black, float fadeOutTime = 0.5f, float fadeTime = 0.5f, float fadeInTime = 0.5f)
    {
        switch (fadeType)
        {
            case FadeType.Black:
                StartCoroutine(C_FadeBlack(inAction, endAction, fadeOutTime, fadeTime, fadeInTime));
            break;
            case FadeType.White:
                StartCoroutine(C_FadeWhite(inAction, endAction, fadeOutTime, fadeTime, fadeInTime));
            break;
            case FadeType.Diamond:
                StartCoroutine(C_FadeDiamond(inAction, endAction, fadeOutTime, fadeTime, fadeInTime));
            break;
        }
    }

    IEnumerator C_FadeBlack(Action inAction, Action endAction, float fadeOutTime, float fadeTime, float fadeInTime)
    {
        _blackPanel.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        _blackPanel.AddToClassList("Black--Fade");
        yield return new WaitForSecondsRealtime(fadeOutTime);

        inAction();
        yield return new WaitForSecondsRealtime(fadeTime);

        _blackPanel.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        _blackPanel.RemoveFromClassList("Black--Fade");
        endAction();
        
        yield return new WaitForSecondsRealtime(fadeInTime);    
    }

    IEnumerator C_FadeWhite(Action inAction, Action endAction, float fadeOutTime, float fadeTime, float fadeInTime)
    {
        _whitePanel.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        _whitePanel.AddToClassList("White--Fade");
        yield return new WaitForSecondsRealtime(fadeOutTime);

        inAction();
        yield return new WaitForSecondsRealtime(fadeTime);

        _whitePanel.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        _whitePanel.RemoveFromClassList("White--Fade");
        endAction();
        
        yield return new WaitForSecondsRealtime(fadeInTime);    
    }

    IEnumerator C_FadeDiamond(Action inAction, Action endAction, float fadeOutTime, float fadeTime, float fadeInTime)
    {
        _diamondPanel[0].style.display = DisplayStyle.Flex;
        _diamondPanel[1].style.display = DisplayStyle.Flex;

        _diamondPanel[0].style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        _diamondPanel[0].AddToClassList("Diamond--Fade");
        yield return new WaitForSecondsRealtime(0.1f);

        _diamondPanel[1].style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        _diamondPanel[1].AddToClassList("Diamond--Fade");
        yield return new WaitForSecondsRealtime(fadeOutTime - 0.1f);

        inAction();
        yield return new WaitForSecondsRealtime(fadeTime);

        _diamondPanel[1].style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        _diamondPanel[1].AddToClassList("Diamond--End");
        yield return new WaitForSecondsRealtime(0.1f);

        _diamondPanel[0].style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        _diamondPanel[0].AddToClassList("Diamond--End");
        endAction();

        yield return new WaitForSecondsRealtime(fadeInTime - 0.1f);

        _diamondPanel[0].style.display = DisplayStyle.None;
        _diamondPanel[1].style.display = DisplayStyle.None;

        yield return new WaitForSecondsRealtime(0.1f);

        _diamondPanel[0].style.transitionDuration = new List<TimeValue> { new (0.1f, TimeUnit.Second) };
        _diamondPanel[0].RemoveFromClassList("Diamond--Fade");
        _diamondPanel[0].RemoveFromClassList("Diamond--End");

        _diamondPanel[1].style.transitionDuration = new List<TimeValue> { new (0.1f, TimeUnit.Second) };
        _diamondPanel[1].RemoveFromClassList("Diamond--Fade");
        _diamondPanel[1].RemoveFromClassList("Diamond--End");
    }

    [Button]
    private void Diamond()
    {
        Fade(() => {}, () => {}, FadeType.Diamond, 0.5f,0.05f,0.5f);
    }
}
