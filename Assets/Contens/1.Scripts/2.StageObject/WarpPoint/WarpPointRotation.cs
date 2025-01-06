using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPointRotation : MonoBehaviour
{
    [SerializeField] float ROTATE_SPEED;

    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(0, 0, ROTATE_SPEED * Time.deltaTime);
    }
}
