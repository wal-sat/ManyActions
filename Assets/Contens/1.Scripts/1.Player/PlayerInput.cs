using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputKind
{
    None, Up, Down, Left, Right,
    S, S_Up, S_Down, S_Left, S_Right,
    E, E_Up, E_Down, E_Left, E_Right,
    W, W_Up, W_Down, W_Left, W_Right,
    N, N_Up, N_Down, N_Left, N_Right,
    L, R
}

public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputManager inputManager;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public bool onUp;
    [HideInInspector] public bool onDown;
    [HideInInspector] public bool onLeft;
    [HideInInspector] public bool onRight;
    [HideInInspector] public bool onS;
    [HideInInspector] public bool onS_Up;
    [HideInInspector] public bool onS_Down;
    [HideInInspector] public bool onS_Left;
    [HideInInspector] public bool onS_Right;
    [HideInInspector] public bool onE;
    [HideInInspector] public bool onE_Up;
    [HideInInspector] public bool onE_Down;
    [HideInInspector] public bool onE_Left;
    [HideInInspector] public bool onE_Right;
    [HideInInspector] public bool onW;
    [HideInInspector] public bool onW_Up;
    [HideInInspector] public bool onW_Down;
    [HideInInspector] public bool onW_Left;
    [HideInInspector] public bool onW_Right;
    [HideInInspector] public bool onN;
    [HideInInspector] public bool onN_Up;
    [HideInInspector] public bool onN_Down;
    [HideInInspector] public bool onN_Left;
    [HideInInspector] public bool onN_Right;
    [HideInInspector] public bool onR;
    [HideInInspector] public bool onL;

    [HideInInspector] public bool onUpPast;
    [HideInInspector] public bool onDownPast;
    [HideInInspector] public bool onLeftPast;
    [HideInInspector] public bool onRightPast;
    [HideInInspector] public bool onSPast;
    [HideInInspector] public bool onS_UpPast;
    [HideInInspector] public bool onS_DownPast;
    [HideInInspector] public bool onS_LeftPast;
    [HideInInspector] public bool onS_RightPast;
    [HideInInspector] public bool onEPast;
    [HideInInspector] public bool onE_UpPast;
    [HideInInspector] public bool onE_DownPast;
    [HideInInspector] public bool onE_LeftPast;
    [HideInInspector] public bool onE_RightPast;
    [HideInInspector] public bool onWPast;
    [HideInInspector] public bool onW_UpPast;
    [HideInInspector] public bool onW_DownPast;
    [HideInInspector] public bool onW_LeftPast;
    [HideInInspector] public bool onW_RightPast;
    [HideInInspector] public bool onNPast;
    [HideInInspector] public bool onN_UpPast;
    [HideInInspector] public bool onN_DownPast;
    [HideInInspector] public bool onN_LeftPast;
    [HideInInspector] public bool onN_RightPast;
    [HideInInspector] public bool onRPast;
    [HideInInspector] public bool onLPast;

    //インゲームのところではこれが呼び出される
    public void PlayerInputUpdate()
    {
        TracePast();

        direction = NormalizeDirection(inputManager.direction);

        CheckInput();
    }

    private Vector2 NormalizeDirection(Vector2 direction)
    {
        if (direction.x > 0.5f) return Vector2.right;
        if (direction.x < -0.5f) return Vector2.left;
        if (direction.y > 0.5f) return Vector2.up;
        if (direction.y < -0.5f) return Vector2.down;

        return Vector2.zero;
    }

    private void CheckInput()
    {
        onUp = false;
        onDown = false;
        onLeft = false;
        onRight = false;
        onS = false;
        onS_Up = false;
        onS_Down = false;
        onS_Left = false;
        onS_Right = false;
        onE = false;
        onE_Up = false;
        onE_Down = false;
        onE_Left = false;
        onE_Right = false;
        onW = false;
        onW_Up = false;
        onW_Down = false;
        onW_Left = false;
        onW_Right = false;
        onN = false;
        onN_Up = false;
        onN_Down = false;
        onN_Left = false;
        onN_Right = false;

        if (inputManager.isPushingSouth)
        {
            if (direction == Vector2.up) onS_Up = true;
            else if (direction == Vector2.down) onS_Down = true;
            else if (direction == Vector2.left) onS_Left = true;
            else if (direction == Vector2.right) onS_Right = true;
            else onS = true;
        }
        else if (inputManager.isPushingEast)
        {
            if (direction == Vector2.up) onE_Up = true;
            else if (direction == Vector2.down) onE_Down = true;
            else if (direction == Vector2.left) onE_Left = true;
            else if (direction == Vector2.right) onE_Right = true;
            else onE = true;
        }
        else if (inputManager.isPushingWest) 
        {
            if (direction == Vector2.up) onW_Up = true;
            else if (direction == Vector2.down) onW_Down = true;
            else if (direction == Vector2.left) onW_Left = true;
            else if (direction == Vector2.right) onW_Right = true;
            else onW = true;
        }
        else if (inputManager.isPushingNorth)
        {
            if (direction == Vector2.up) onN_Up = true;
            else if (direction == Vector2.down) onN_Down = true;
            else if (direction == Vector2.left) onN_Left = true;
            else if (direction == Vector2.right) onN_Right = true;
            else onN = true;
        }
        else
        {
            if (direction == Vector2.up) onUp = true;
            else if (direction == Vector2.down) onDown = true;
            else if (direction == Vector2.left) onLeft = true;
            else if (direction == Vector2.right) onRight = true;
        }

        onR = inputManager.isPushingR;
        onL = inputManager.isPushingL;
    }

    private void TracePast()
    {
        onUpPast = onUp;
        onDownPast = onDown;
        onLeftPast = onLeft;
        onRightPast = onRight;
        onSPast = onS;
        onS_UpPast = onS_Up;
        onS_DownPast = onS_Down;
        onS_LeftPast = onS_Left;
        onS_RightPast = onS_Right;
        onEPast = onE;
        onE_UpPast = onE_Up;
        onE_DownPast = onE_Down;
        onE_LeftPast = onE_Left;
        onE_RightPast = onE_Right;
        onWPast = onW;
        onW_UpPast = onW_Up;
        onW_DownPast = onW_Down;
        onW_LeftPast = onW_Left;
        onW_RightPast = onW_Right;
        onNPast = onN;
        onN_UpPast = onN_Up;
        onN_DownPast = onN_Down;
        onN_LeftPast = onN_Left;
        onN_RightPast = onN_Right;
        onRPast = onR;
        onLPast = onL;
    }
}