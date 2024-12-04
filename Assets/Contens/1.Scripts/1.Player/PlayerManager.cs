using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimationManager playerAnimationManager;
    [SerializeField] public PlayerActionManager PlayerActionManager;
    [SerializeField] PlayerPreventStuck playerPreventStuck;

    [SerializeField] public GameObject Player;

    [HideInInspector] public bool isMovingPlayer;
    private void FixedUpdate()
    {
        playerAnimationManager.AnimationUpdate(isMovingPlayer);
        
        if (isMovingPlayer) 
        {
            playerMovement.MovementUpdate();
            PlayerActionManager.ActionUpdate();
            playerPreventStuck.PreventStuckUpdate();
        }
    }

    public void Initialize(bool facingRight)
    {
        isMovingPlayer = false;

        playerMovement.Initialize(facingRight);
        playerAnimationManager.Initialize();
        PlayerActionManager.Initialize();
        playerPreventStuck.Initialize();
    }

    public void Door()
    {
        isMovingPlayer = false;

        playerMovement.Initialize(playerMovement.isFacingRight);
        PlayerActionManager.Initialize();
    }
}
