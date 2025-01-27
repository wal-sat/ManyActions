using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerAnimationManager playerAnimationManager;
    [SerializeField] public PlayerActionManager PlayerActionManager;
    [SerializeField] public PlayerKidouUI playerKidouUI;
    [SerializeField] PlayerPreventStuck playerPreventStuck;
    [SerializeField] PlayerOutCameraDie PlayerOutCameraDie;

    [SerializeField] public GameObject Player;

    [HideInInspector] public bool isMovingPlayer;

    private bool _isDoorEntering;


    private void FixedUpdate()
    {
        if (_isDoorEntering) return;

        playerAnimationManager.AnimationUpdate(isMovingPlayer);
        
        if (isMovingPlayer) 
        {
            playerMovement.MovementUpdate();
            PlayerActionManager.ActionUpdate();
            playerPreventStuck.PreventStuckUpdate();
            PlayerOutCameraDie.OutCameraUpdate();
        }
    }

    public void Initialize(bool isFacingRight)
    {
        isMovingPlayer = false;

        playerMovement.Initialize(isFacingRight);
        playerAnimationManager.Initialize(isFacingRight);
        PlayerActionManager.Initialize();
        playerPreventStuck.Initialize();

        playerKidouUI.SetActiveTrue(isFacingRight);
    }

    public void Door()
    {
        _isDoorEntering = true;

        playerMovement.Initialize(playerMovement.isFacingRight);
        PlayerActionManager.Initialize();
    }
}
