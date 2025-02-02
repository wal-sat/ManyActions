using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] GameObject virtualCamera;

    private void FixedUpdate()
    {
        if (virtualCamera == null) return;
        this.gameObject.transform.position = new Vector3 (virtualCamera.transform.position.x, virtualCamera.transform.position.y, this.gameObject.transform.position.z);   
    }
}
