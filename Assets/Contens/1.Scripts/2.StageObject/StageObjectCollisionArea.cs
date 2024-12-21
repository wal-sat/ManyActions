using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObjectCollisionArea : MonoBehaviour
{
    public Action triggerEnter;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            triggerEnter();
        }
    }
}
