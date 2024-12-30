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
    private const float EXIT_BUFFER = 0.1f;

    private float _timer;
    private bool _isTimer;

    private void Start()
    {
        playerMovement._conveyorPlusSpeed = PLUS_SPEED;
    }
    private void FixedUpdate()
    {
        if (_isTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > EXIT_BUFFER)
            {
                _timer = 0;
                _isTimer = false;

                playerMovement.isLandingConveyor = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= PlayerLandingChecker.position.y) 
            {
                if (IS_RIGHT_DIRECTION) playerMovement.isLandingConveyor_right = true;
                if (!IS_RIGHT_DIRECTION) playerMovement.isLandingConveyor_left = true;

                playerMovement.isLandingConveyor = true;

                _timer = 0;
                _isTimer = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isTimer = true;
        }
    }
}
