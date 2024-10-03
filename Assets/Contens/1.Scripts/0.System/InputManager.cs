using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;

    [HideInInspector] public bool canInput;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public bool isPushingSouth;
    [HideInInspector] public bool isPushingEast;
    [HideInInspector] public bool isPushingWest;
    [HideInInspector] public bool isPushingNorth;
    [HideInInspector] public bool isPushingR;
    [HideInInspector] public bool isPushingL;
    [HideInInspector] public bool isPushingOption;

    [HideInInspector] public Vector2 move;
    [HideInInspector] public bool isPushingSelect;
    [HideInInspector] public bool isPushingCancel;
    
    private void Awake()
    {
        canInput = true; //後に消すかもね～～～！！！！
        SwitchActionMap("Player");
    }

    public void SwitchActionMap(string actionMapName)
    {
        foreach (var map in inputActionAsset.actionMaps)
        {
            map.Disable();
        }

        InputActionMap selectedMap = inputActionAsset.FindActionMap(actionMapName);
        if (selectedMap != null) selectedMap.Enable();
    }

    //ーーーーーPLayer Mapーーーーー
    public void Direction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            direction = Vector2.zero;
        }

        if (!canInput) direction = Vector2.zero;
    }
    public void ActionSouth(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingSouth = true;
        else if (context.canceled) isPushingSouth = false;

        if (!canInput) isPushingSouth = false;
    }
    public void ActionEast(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingEast = true;
        else if (context.canceled) isPushingEast = false;

        if (!canInput) isPushingEast = false;
    }
    public void ActionWest(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingWest = true;
        else if (context.canceled) isPushingWest = false;

        if (!canInput) isPushingWest = false;
    }
    public void ActionNorth(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingNorth = true;
        else if (context.canceled) isPushingNorth = false;

        if (!canInput) isPushingNorth = false;
    }
    public void ActionRight(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingR = true;
        else if (context.canceled) isPushingR = false;

        if (!canInput) isPushingR = false;
    }
    public void ActionLeft(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingL = true;
        else if (context.canceled) isPushingL = false;

        if (!canInput) isPushingL = false;
    }
    public void Option(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingOption = true;
        else if (context.canceled) isPushingOption = false;

        if (!canInput) isPushingOption = false;
    }

    //ーーーーーUI Mapーーーーー
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            move = context.ReadValue<Vector2>();
        }

        if (!canInput) move = Vector2.zero;
    }
    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingSelect = true;
        else if (context.canceled) isPushingSelect = false;

        if (!canInput) isPushingSelect = false;
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingCancel = true;
        else if (context.canceled) isPushingCancel = false;

        if (!canInput) isPushingCancel = false;
    }
}
