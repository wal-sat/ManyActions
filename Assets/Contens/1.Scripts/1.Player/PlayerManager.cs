using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] public PlayerActionManager PlayerActionManager;
    [SerializeField] public GameObject Player;

    [HideInInspector] public bool isMovingPlayer;
    private void FixedUpdate()
    {
        playerAnimation.AnimationUpdate(isMovingPlayer);
        
        if (isMovingPlayer) 
        {
            playerMovement.MovementUpdate();
            PlayerActionManager.ActionUpdate();
        }
    }

    public void Initialize(bool facingRight)
    {
        isMovingPlayer = false;

        playerMovement.Initialize(facingRight);
        playerAnimation.Initialize();
        PlayerActionManager.Initialize();
    }

    public void Door()
    {
        isMovingPlayer = false;

        playerMovement.Initialize(playerMovement.isFacingRight);
        PlayerActionManager.Initialize();
    }
}
