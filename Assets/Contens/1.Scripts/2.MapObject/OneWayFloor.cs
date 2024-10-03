using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayFloor : MonoBehaviour
{
    [SerializeField] S_Down_GoDown Manager;
    [SerializeField] Transform PlayerLandingChecker;
    [SerializeField] Collider2D selfCollider;

    [HideInInspector] public bool onS_Down;

    private void Start()
    {
        Manager.Register(this);
    }

    private void Update()
    {
        if (onS_Down)
        {
            selfCollider.enabled = false;
            return;   
        }

        if (this.transform.position.y > PlayerLandingChecker.position.y) selfCollider.enabled = false;
        else selfCollider.enabled = true;
    }
}
