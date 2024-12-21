using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DieParts : MonoBehaviour
{
    [SerializeField] Transform PlayerPosition;
    [SerializeField] float DESTROY_DISTANCE;

    private void Update() 
    {
        if (Vector3.Distance(PlayerPosition.position, this.gameObject.transform.position) > DESTROY_DISTANCE)
        {
            Destroy(this.gameObject);
        }
    }
}
