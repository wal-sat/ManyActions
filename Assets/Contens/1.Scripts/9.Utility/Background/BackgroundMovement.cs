using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] GameObject mianCamera;

    private void FixedUpdate()
    {
        if (mianCamera == null) return;
        this.gameObject.transform.position = new Vector3 (mianCamera.transform.position.x, mianCamera.transform.position.y, this.gameObject.transform.position.z);   
    }
}
