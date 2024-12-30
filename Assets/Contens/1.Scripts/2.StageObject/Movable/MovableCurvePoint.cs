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
                    if (other.transform.position.y < this.transform.position.y) movableObject.Curve(movableDirection);
                break;
                case MovableDirection.down:
                    if (other.transform.position.y > this.transform.position.y) movableObject.Curve(movableDirection);
                break;
                case MovableDirection.left:
                    if (other.transform.position.x > this.transform.position.x) movableObject.Curve(movableDirection);
                break;
                case MovableDirection.right:
                    if (other.transform.position.x < this.transform.position.x) movableObject.Curve(movableDirection);
                break;
            }
        } 
    }
}
