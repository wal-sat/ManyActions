using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionNBase : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    public void EndAction()
    {
        if (playerInput.onN || playerInput.onN_Up || playerInput.onN_Left || playerInput.onN_Right || playerInput.onN_Down)
        {

        }
    }
}
