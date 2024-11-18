using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

public enum FadeKind { Black, White }

public class S_FadeManager : Singleton<S_FadeManager>
{
    private GameObject _UIToolkit;

    private VisualElement _fadePanel;

    public void FadeInOut(Action inAction, Action endAction, float fadeOutTime = 0.5f, float fadeTime = 1f, float fadeInTime = 0.5f, FadeKind fadeKind = FadeKind.Black)
    {
        StartCoroutine(C_FadeInOut(inAction, endAction, fadeOutTime, fadeTime, fadeInTime, fadeKind));
    }
    IEnumerator C_FadeInOut(Action inAction, Action endAction, float fadeOutTime, float fadeTime, float fadeInTime, FadeKind fadeKind)
    {
        _UIToolkit = GameObject.Find("UIToolkit-Fade");
        if (_UIToolkit == null) yield break;

        var root = _UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        string classname = "";

        switch (fadeKind)
        {
            case FadeKind.Black:
                _fadePanel = root.Q<VisualElement>("Black");
                classname = "Black--Fade";
            break;
            case FadeKind.White:
                _fadePanel = root.Q<VisualElement>("White");
                classname = "White--Fade";
            break;
        }

        _fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };;
        _fadePanel.AddToClassList(classname);
        yield return new WaitForSeconds(fadeOutTime);

        inAction();
        yield return new WaitForSeconds(fadeTime);

        _fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };;
        _fadePanel.RemoveFromClassList(classname);
        yield return new WaitForSeconds(fadeInTime);

        endAction();
    }

    public void FadeOut(Action endAction, float fadeOutTime = 0.5f, FadeKind fadeKind = FadeKind.Black)
    {
        StartCoroutine(C_FadeOut(endAction, fadeOutTime, fadeKind));
    }
    IEnumerator C_FadeOut(Action endAction, float fadeOutTime, FadeKind fadeKind)
    {
        _UIToolkit = GameObject.Find("UIToolkit-Fade");
        if (_UIToolkit == null) yield break;

        var root = _UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        string classname = "";

        switch (fadeKind)
        {
            case FadeKind.Black:
                _fadePanel = root.Q<VisualElement>("Black");
                classname = "Black--Fade";
            break;
            case FadeKind.White:
                _fadePanel = root.Q<VisualElement>("White");
                classname = "White--Fade";
            break;
        }

        _fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeOutTime, TimeUnit.Second) };;
        _fadePanel.AddToClassList(classname);
        yield return new WaitForSeconds(fadeOutTime);

        endAction();
    }

    public void FadeIn(Action endAction, float fadeInTime = 0.5f, FadeKind fadeKind = FadeKind.Black)
    {
        StartCoroutine(C_FadeIn(endAction, fadeInTime, fadeKind));
    }
    IEnumerator C_FadeIn(Action endAction, float fadeInTime, FadeKind fadeKind)
    {
        _UIToolkit = GameObject.Find("UIToolkit-Fade");
        if (_UIToolkit == null) yield break;

        var root = _UIToolkit.GetComponent<UIDocument>().rootVisualElement;

        string classname = "";

        switch (fadeKind)
        {
            case FadeKind.Black:
                _fadePanel = root.Q<VisualElement>("Black");
                classname = "Black--Fade";
            break;
            case FadeKind.White:
                _fadePanel = root.Q<VisualElement>("White");
                classname = "White--Fade";
            break;
        }

        _fadePanel.style.transitionDuration = new List<TimeValue> { new (fadeInTime, TimeUnit.Second) };;
        _fadePanel.RemoveFromClassList(classname);
        yield return new WaitForSeconds(fadeInTime);

        endAction();
    }

    [Button]
    private void Out()
    {
        FadeOut(() => Debug.Log("end"), 2, FadeKind.White);
    }
    [Button]
    private void In()
    {
        FadeIn(() => Debug.Log("end"), 1, FadeKind.White);
    }
}
