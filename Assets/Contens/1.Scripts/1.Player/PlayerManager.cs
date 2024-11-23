using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] PlayerActionManager PlayerActionManager;

    [HideInInspector] public bool _isMovingPlayer;
    private void FixedUpdate()
    {
        if (_isMovingPlayer) 
        {
            playerMovement.MovementUpdate();
            playerAnimation.AnimationUpdate();
            PlayerActionManager.ActionUpdate();
        }
    }

    public void Initialize(bool facingRight)
    {
        _isMovingPlayer = false;

        playerMovement.Initialize(facingRight);
        playerAnimation.Initialize();
        PlayerActionManager.Initialize();
    }
}
