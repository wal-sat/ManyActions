using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCurvePoint : MonoBehaviour
{
    [SerializeField] MovableDirection pastDirection;
    [SerializeField] MovableDirection movableDirection;

    private void OnTriggerStay2D(Collider2D other)
    {
        IMovable movableObject = other.gameObject.GetComponent<IMovable>();
        if (movableObject != null)
        {
            switch (pastDirection)
            {
                case MovableDirection.up:
                    if (other.transform.position.y < this.transform.position.y) DirectionChange(movableObject, other);
                break;
                case MovableDirection.down:
                    if (other.transform.position.y > this.transform.position.y) DirectionChange(movableObject, other);
                break;
                case MovableDirection.left:
                    if (other.transform.position.x > this.transform.position.x) DirectionChange(movableObject, other);
                break;
                case MovableDirection.right:
                    if (other.transform.position.x < this.transform.position.x) DirectionChange(movableObject, other);
                break;
                case MovableDirection.up_left:
                    if (other.transform.position.y < this.transform.position.y && other.transform.position.x < this.transform.position.x) DirectionChange(movableObject, other);
                break;
                case MovableDirection.up_right:
                    if (other.transform.position.y < this.transform.position.y && other.transform.position.x > this.transform.position.x) DirectionChange(movableObject, other);
                break;
                case MovableDirection.down_left:
                    if (other.transform.position.y > this.transform.position.y && other.transform.position.x < this.transform.position.x) DirectionChange(movableObject, other);
                break;
                case MovableDirection.down_right:
                    if (other.transform.position.y > this.transform.position.y && other.transform.position.x > this.transform.position.x) DirectionChange(movableObject, other);
                break;
            }
        } 
    }

    private void DirectionChange(IMovable movableObject, Collider2D other)
    {
        movableObject.Curve(movableDirection); 
        other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z);
    }
}
