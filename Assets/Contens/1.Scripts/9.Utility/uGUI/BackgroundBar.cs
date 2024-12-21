using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundBar : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform LeftTransform;
    [SerializeField] Transform RightTransform;
    [SerializeField] float SPEED;

    private float _LeftRightDistance;

    private void Awake()
    {
        _LeftRightDistance = RightTransform.localPosition.x - LeftTransform.localPosition.x;
    }

    private void FixedUpdate()
    {
        if (playerMovement == null)
        {
            this.gameObject.transform.localPosition += new Vector3(-SPEED * Time.deltaTime, 0f ,0f); 
        }
        else
        {
            if (playerMovement.isFacingRight) this.gameObject.transform.localPosition += new Vector3(-SPEED * Time.deltaTime, 0f ,0f); 
            else this.gameObject.transform.localPosition += new Vector3(SPEED * Time.deltaTime, 0f ,0f); 

        }

        if (this.gameObject.transform.localPosition.x < LeftTransform.localPosition.x)
        {
            this.gameObject.transform.localPosition += new Vector3(_LeftRightDistance, 0f, 0f);
        }
        if (RightTransform.localPosition.x < this.gameObject.transform.localPosition.x)
        {
            this.gameObject.transform.localPosition += new Vector3(-_LeftRightDistance, 0f, 0f);
        }   
    }
}
