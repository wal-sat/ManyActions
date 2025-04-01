using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMapKind { Player, UI }

public class S_InputSystem : Singleton<S_InputSystem>
{
    [SerializeField] InputActionAsset inputActionAsset;

    [HideInInspector] public bool canInput;

    [HideInInspector] public Vector2 leftDirection;
    [HideInInspector] public Vector2 rightDirection;
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
    public void LeftDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftDirection = NormalizeDirection( context.ReadValue<Vector2>() );
        }
        else if (context.canceled)
        {
            leftDirection = Vector2.zero;
        }

        if (!canInput) leftDirection = Vector2.zero;
    }
    public void RightDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rightDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            rightDirection = Vector2.zero;
        }

        if (!canInput) rightDirection = Vector2.zero;
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
        else if (direction.x < -0.5f) return Vector2.left;
        else if (direction.y > 0.5f) return Vector2.up;
        else if (direction.y < -0.5f) return Vector2.down;

        return Vector2.zero;
    }
}

