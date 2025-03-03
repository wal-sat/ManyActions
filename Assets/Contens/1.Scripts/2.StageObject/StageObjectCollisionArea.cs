using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObjectCollisionArea : MonoBehaviour
{
    public Action triggerEnter;
    public Action triggerExit;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (triggerEnter != null) triggerEnter();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (triggerExit != null) triggerExit();
        }
    }
}
