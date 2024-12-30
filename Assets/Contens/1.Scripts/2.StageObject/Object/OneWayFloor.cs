using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayFloor : MonoBehaviour
{
    [SerializeField] S_Down_GoDown Manager;
    [SerializeField] Transform PlayerLandingChecker;
    [SerializeField] Collider2D selfCollider;
    [SerializeField] float OFFSET_Y;
    [SerializeField] float BUFFER_TIME;

    [HideInInspector] public bool onS_Down;

    private void Start()
    {
        Manager.Register(this);
    }

    private void Update()
    {
        if (onS_Down)
        {
            StopAllCoroutines();
            selfCollider.enabled = false;
            return;   
        }

        if (this.transform.position.y + OFFSET_Y > PlayerLandingChecker.position.y) 
        {
            StopAllCoroutines();
            selfCollider.enabled = false;
        }
        else
        {
            if (!selfCollider.enabled) StartCoroutine(ColliderEnable(BUFFER_TIME));
        }
    }

    IEnumerator ColliderEnable(float bufferTime)
    {
        yield return new WaitForSeconds(bufferTime);

        selfCollider.enabled = true;
    }
}
