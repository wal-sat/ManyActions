using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRamp : MonoBehaviour
{
    [SerializeField] Transform PlayerLandingChecker;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float JUMP_POWER;

    private const float OFFSET = 0.2f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= PlayerLandingChecker.position.y) 
            {
                rb.velocity = new Vector3(rb.velocity.x, JUMP_POWER * Time.deltaTime, 0);
            }
        }
    }
}
