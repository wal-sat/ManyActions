using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour, IMovable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] MovableDirection INIT_DIRECTION;
    [SerializeField] float SPEED;

    private MovableDirection _movableDirection;
    private bool isGenerated;

    private void Start()
    {
        if (isGenerated) return;
        _movableDirection = INIT_DIRECTION;
    }

    private void FixedUpdate()
    {
        switch (_movableDirection)
        {
            case MovableDirection.up:
                rb.velocity = new Vector2(0, SPEED * Time.deltaTime);
            break;
            case MovableDirection.down:
                rb.velocity = new Vector2(0, -1 * SPEED * Time.deltaTime);
            break;
            case MovableDirection.left:
                rb.velocity = new Vector2(-1 * SPEED * Time.deltaTime, 0);
            break;
            case MovableDirection.right:
                rb.velocity = new Vector2(SPEED * Time.deltaTime, 0);
            break;
            case MovableDirection.up_left:
                rb.velocity = new Vector2(-1 * SPEED * Time.deltaTime, SPEED * Time.deltaTime);
            break;
            case MovableDirection.up_right:
                rb.velocity = new Vector2(SPEED * Time.deltaTime, SPEED * Time.deltaTime);
            break;
            case MovableDirection.down_left:
                rb.velocity = new Vector2(-1 * SPEED * Time.deltaTime, -1 * SPEED * Time.deltaTime);
            break;
            case MovableDirection.down_right:
                rb.velocity = new Vector2(SPEED * Time.deltaTime, -1 * SPEED * Time.deltaTime);
            break;
            default:
                rb.velocity = Vector2.zero;
            break;
        }
    }

    public void Init(MovableDirection movableDirection)
    {
        isGenerated = true;
        _movableDirection = movableDirection;
    }
    public void Curve(MovableDirection movableDirection)
    {
        _movableDirection = movableDirection;
    }
}
