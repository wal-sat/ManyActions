using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGameScene : MonoBehaviour
{
    [SerializeField] GameSceneUI gameSceneUI;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] SavePointManager savePointManager;
    [SerializeField] SectionManager sectionManager;

    [SerializeField] int startSection = -1;
    [SerializeField] bool isUIInvisible;

    private void Start()
    {
        if (isUIInvisible) InvokeRepeating( ( (Action) UIInvisible).Method.Name, 0, 1f );

        if (startSection != -1) Invoke( ( (Action) StartSectionChange).Method.Name, 0.1f );
    }

    private void StartSectionChange()
    {
        if (sectionManager != null)
        {
            savePointManager.TeleportStartPosition( sectionManager.ChangeSection(startSection) );
            playerManager.Initialize( savePointManager.savePoint.facingRight );
        }
    }
    private void UIInvisible()
    {
        gameSceneUI.Debug_UIInvisible();
    }
}
