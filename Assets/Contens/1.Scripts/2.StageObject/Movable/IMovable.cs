using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovableDirection { up, down, left, right, up_left, up_right, down_left, down_right }

public interface IMovable
{
    public void Init(MovableDirection movableDirection, float speed);
    public void Curve(MovableDirection movableDirection);
}
