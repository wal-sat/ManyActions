using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMenu : MonoBehaviour
{
    private void Start()
    {   
        S_InputSystem._instance.canInput = true;
        S_InputSystem._instance.SwitchActionMap("UI");
    }

    
}
