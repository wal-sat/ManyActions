using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] Transform PlayerLandingChecker;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float PLUS_SPEED;
    [SerializeField] bool IS_RIGHT_DIRECTION;

    private const float OFFSET = 0.2f;

    private void Start()
    {
        playerMovement._conveyorPlusSpeed = PLUS_SPEED;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= PlayerLandingChecker.position.y) 
            {
                if (IS_RIGHT_DIRECTION) playerMovement.isLandingConveyor_right = true;
                if (!IS_RIGHT_DIRECTION) playerMovement.isLandingConveyor_left = true;

                playerMovement.isLandingConveyor = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= PlayerLandingChecker.position.y) 
            {
                playerMovement.isLandingConveyor = false;
            }
        }
    }
}
