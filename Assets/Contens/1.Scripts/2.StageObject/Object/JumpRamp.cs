using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRamp : MonoBehaviour
{
    [SerializeField] private float JUMP_POWER;

    private Transform _playerLandingChecker;
    private Rigidbody2D _rb;
    private PlayerMovement _playerMovement;

    private const float OFFSET = 0.5f;
    private FirstCallChecker _firstCallChecker = new FirstCallChecker();

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            _playerLandingChecker = other.gameObject.transform.Find("_LandingChecker").gameObject.transform;
            _rb = other.gameObject.GetComponent<Rigidbody2D>();
            _playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= _playerLandingChecker.position.y )
            {
                if (_firstCallChecker.Check())
                {
                    _rb.velocity = new Vector3(_rb.velocity.x, JUMP_POWER * Time.deltaTime, 0);

                    S_SEManager._instance.Play("s_jumpRamp");
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        _firstCallChecker.Reset();
    }
}
