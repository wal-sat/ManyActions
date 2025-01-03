using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMapKind { Player, UI }

public class S_InputSystem : Singleton<S_InputSystem>
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

    public void SwitchActionMap(ActionMapKind actionMap)
    {
        foreach (var map in inputActionAsset.actionMaps)
        {
            map.Disable();
        }

        string actionMapName = "";
        if (actionMap == ActionMapKind.Player) actionMapName = "Player";
        if (actionMap == ActionMapKind.UI) actionMapName = "UI";

        InputActionMap selectedMap = inputActionAsset.FindActionMap(actionMapName);
        if (selectedMap != null) selectedMap.Enable();

        foreach (var _actionMap in inputActionAsset.actionMaps)
        {
            if (_actionMap.enabled)
            {
                Debug.Log("現在有効なAction Map: " + _actionMap.name);
            }
        }
    }

    public ActionMapKind CurrentActionMap()
    {
        foreach (var actionMap in inputActionAsset.actionMaps)
        {
            if (actionMap.enabled)
            {
                if (actionMap.name == "Player") return ActionMapKind.Player;
                else if (actionMap.name == "UI") return ActionMapKind.UI;
            }
        }
        return ActionMapKind.Player;
    }

    //ーーーーーPLayer Mapーーーーー
    public void Direction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = NormalizeDirection( context.ReadValue<Vector2>() );
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
        if (context.performed) move = NormalizeDirection( context.ReadValue<Vector2>() );
        else move = Vector2.zero;

        if (!canInput) move = Vector2.zero;
    }
    public void Select(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingSelect = true;
        else isPushingSelect = false;

        if (!canInput) isPushingSelect = false;
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        if (context.performed) isPushingCancel = true;
        else isPushingCancel = false;

        if (!canInput) isPushingCancel = false;
    }

    //ーーーーーVector2の正規化ーーーーー
    private Vector2 NormalizeDirection(Vector2 direction)
    {
        if (direction.x > 0.5f) return Vector2.right;
        if (direction.x < -0.5f) return Vector2.left;
        if (direction.y > 0.5f) return Vector2.up;
        if (direction.y < -0.5f) return Vector2.down;

        return Vector2.zero;
    }
}

