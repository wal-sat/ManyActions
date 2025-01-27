using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableTerminal : MonoBehaviour
{
    [SerializeField] MovableDirection pastDirection;

    private void OnTriggerStay2D(Collider2D other)
    {
        IMovable movableObject = other.gameObject.GetComponent<IMovable>();
        if (movableObject != null)
        {
            switch (pastDirection)
            {
                case MovableDirection.up:
                    if (other.transform.position.y < this.transform.position.y) Destroy(other.gameObject);
                break;
                case MovableDirection.down:
                    if (other.transform.position.y > this.transform.position.y) Destroy(other.gameObject);
                break;
                case MovableDirection.left:
                    if (other.transform.position.x > this.transform.position.x) Destroy(other.gameObject);
                break;
                case MovableDirection.right:
                    if (other.transform.position.x < this.transform.position.x) Destroy(other.gameObject);
                break;
                case MovableDirection.up_left:
                    if (other.transform.position.y < this.transform.position.y && other.transform.position.x < this.transform.position.x) Destroy(other.gameObject);
                break;
                case MovableDirection.up_right:
                    if (other.transform.position.y < this.transform.position.y && other.transform.position.x > this.transform.position.x) Destroy(other.gameObject);
                break;
                case MovableDirection.down_left:
                    if (other.transform.position.y > this.transform.position.y && other.transform.position.x < this.transform.position.x) Destroy(other.gameObject);
                break;
                case MovableDirection.down_right:
                    if (other.transform.position.y > this.transform.position.y && other.transform.position.x > this.transform.position.x) Destroy(other.gameObject);
                break;
            }
        } 
    }
}
