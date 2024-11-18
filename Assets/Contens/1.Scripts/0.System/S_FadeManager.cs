using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public enum FadeKind { InOut, In, Out }
public enum FadeType { Black, White, Diamond }

public class S_FadeManager : Singleton<S_FadeManager>
{
    public void FadeInOut(Action inAction, Action endAction, FadeType fadeType = FadeType.Black, float fadeOutTime = 0.5f, float fadeTime = 0.5f, float fadeInTime = 0.5f)
    {
        switch (fadeType)
        {
            case FadeType.Black:
            case FadeType.White:
                StartCoroutine(C_Fade(inAction, endAction, FadeKind.InOut, fadeType, fadeOutTime, fadeTime, fadeInTime));
            break;
            case FadeType.Diamond:
                StartCoroutine(C_FadeDiamond(inAction, endAction, fadeOutTime, fadeTime, fadeInTime));

            break;
        }
    }
    public void FadeIn(Action inAction, Action endAction, FadeType fadeType = FadeType.Black, float fadeOutTime = 0.5f, float fadeTime = 0.5f, float fadeInTime = 0.5f)
    {
        StartCoroutine(C_Fade(inAction, endAction, FadeKind.In, fadeType, fadeOutTime, fadeTime, fadeInTime));
    }
    public void FadeOut(Action inAction, Action endAction, FadeType fadeType = FadeType.Black, float fadeOutTime = 0.5f, float fadeTime = 0.5f, float fadeInTime = 0.5f)
    {
        StartCoroutine(C_Fade(inAction, endAction, FadeKind.Out, fadeType, fadeOutTime, fadeTime, fadeInTime));
    }

    public void Void() { }

    IEnumerator C_Fade(Action inAction, Action endAction, FadeKind fadeKind, FadeType fadeType, float fadeOutTime, float fadeTime, float fadeInTime)
    {
        GameObject UIToolkit = GameObject.Find("UIToolkit-Fade");
        if (UIToolkit == null) yield break;

        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        string classname;
        VisualElement fadePanel;

        switch (fadeType)
        {
            case FadeType.Black:
            default:
                fadePanel = root.Q<VisualElement>("Black");
                classname = "Black--Fade";
            break;
            case FadeType.White:
                fadePanel = root.Q<VisualElement>("White");
                classname = "White--Fade";
            break;
        }

        if (fadeKind == FadeKind.InOut || fadeKind == FadeKind.Out)
        {
            fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
            fadePanel.AddToClassList(classname);
            yield return new WaitForSeconds(fadeOutTime);

            inAction();
            yield return new WaitForSeconds(fadeTime);
        }

        if (fadeKind == FadeKind.InOut || fadeKind == FadeKind.In)
        {
            fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
            fadePanel.RemoveFromClassList(classname);
            yield return new WaitForSeconds(fadeInTime);
            
            endAction();
        }
    }

    IEnumerator C_FadeDiamond(Action inAction, Action endAction, float fadeOutTime, float fadeTime, float fadeInTime)
    {
        GameObject UIToolkit = GameObject.Find("UIToolkit-Fade");
        if (UIToolkit == null) yield break;

        var root = UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        VisualElement fadePanel_1 = root.Q<VisualElement>("Diamond");
        VisualElement fadePanel_2 = root.Q<VisualElement>("Diamond_White");
        string classname_1 = "Diamond--Fade";
        string classname_2 = "Diamond--End";

        fadePanel_1.style.display = DisplayStyle.Flex;
        fadePanel_2.style.display = DisplayStyle.Flex;

        fadePanel_1.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        fadePanel_1.AddToClassList(classname_1);
        yield return new WaitForSeconds(0.1f);

        fadePanel_2.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };
        fadePanel_2.AddToClassList(classname_1);
        yield return new WaitForSeconds(fadeOutTime - 0.1f);

        inAction();
        yield return new WaitForSeconds(fadeTime);

        fadePanel_2.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        fadePanel_2.AddToClassList(classname_2);
        yield return new WaitForSeconds(0.1f);

        fadePanel_1.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };
        fadePanel_1.AddToClassList(classname_2);
        yield return new WaitForSeconds(fadeInTime - 0.1f);

        endAction();

        fadePanel_1.style.display = DisplayStyle.None;
        fadePanel_2.style.display = DisplayStyle.None;

        yield return new WaitForSeconds(0.1f);

        fadePanel_1.style.transitionDuration = new List<TimeValue> { new (0.1f, TimeUnit.Second) };
        fadePanel_1.RemoveFromClassList(classname_1);
        fadePanel_1.RemoveFromClassList(classname_2);

        fadePanel_2.style.transitionDuration = new List<TimeValue> { new (0.1f, TimeUnit.Second) };
        fadePanel_2.RemoveFromClassList(classname_1);
        fadePanel_2.RemoveFromClassList(classname_2);
    }

    [Button]
    private void Diamond()
    {
        FadeInOut(() => {}, () => {}, FadeType.Diamond, 0.5f,0.1f,0.5f);
    }
    [Button]
    private void Out()
    {
        FadeOut(() => {}, () => {}, FadeType.Black);
    }
    [Button]
    private void In()
    {
        FadeIn(() => {}, () => {}, FadeType.Black);
    }
}
