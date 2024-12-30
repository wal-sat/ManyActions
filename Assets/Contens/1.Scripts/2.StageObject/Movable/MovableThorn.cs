using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableThorn : AttackableBase, IMovable
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float ROTATE_SPEED;

    private MovableDirection _movableDirection;
    private float _speed;

    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(0f, 0f, ROTATE_SPEED * Time.deltaTime, Space.World );

        switch (_movableDirection)
        {
            case MovableDirection.up:
                rb.velocity = new Vector2(0, _speed * Time.deltaTime);
            break;
            case MovableDirection.down:
                rb.velocity = new Vector2(0, -1 * _speed * Time.deltaTime);
            break;
            case MovableDirection.left:
                rb.velocity = new Vector2(-1 * _speed * Time.deltaTime, 0);
            break;
            case MovableDirection.right:
                rb.velocity = new Vector2(_speed * Time.deltaTime, 0);
            break;
            case MovableDirection.up_left:
                rb.velocity = new Vector2(-1 * _speed * Time.deltaTime, _speed * Time.deltaTime);
            break;
            case MovableDirection.up_right:
                rb.velocity = new Vector2(_speed * Time.deltaTime, _speed * Time.deltaTime);
            break;
            case MovableDirection.down_left:
                rb.velocity = new Vector2(-1 * _speed * Time.deltaTime, -1 * _speed * Time.deltaTime);
            break;
            case MovableDirection.down_right:
                rb.velocity = new Vector2(_speed * Time.deltaTime, -1 * _speed * Time.deltaTime);
            break;
            default:
                rb.velocity = Vector2.zero;
            break;
        }
    }

    public void Init(MovableDirection movableDirection, float speed)
    {
        _movableDirection = movableDirection;
        _speed = speed;
    }
    public void Curve(MovableDirection movableDirection)
    {
        _movableDirection = movableDirection;
    }
}
