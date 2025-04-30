using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameScene : MonoBehaviour
{
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] bool isUIInvisible;

    private void Start()
    {
        if (isUIInvisible) InvokeRepeating(((Action) UIInvisible).Method.Name, 0, 1f);   
    }

    private void UIInvisible()
    {
        gameSceneUI.Debug_UIInvisible();
    }
}
