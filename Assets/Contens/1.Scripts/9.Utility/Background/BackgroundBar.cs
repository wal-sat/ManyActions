using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundBar : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform LeftTransform;
    [SerializeField] Transform RightTransform;
    [SerializeField] float SPEED;

    private float _LeftRightDistance;

    private void Awake()
    {
        _LeftRightDistance = RightTransform.position.x - LeftTransform.position.x;
    }

    private void FixedUpdate()
    {
        if (playerMovement == null)
        {
            this.gameObject.transform.position += new Vector3(-SPEED * Time.deltaTime, 0f ,0f); 
        }
        else
        {
            if (playerMovement.isFacingRight) this.gameObject.transform.position += new Vector3(-SPEED * Time.deltaTime, 0f ,0f); 
            else this.gameObject.transform.position += new Vector3(SPEED * Time.deltaTime, 0f ,0f); 
        }

        if (this.gameObject.transform.position.x < LeftTransform.position.x)
        {
            this.gameObject.transform.position += new Vector3(_LeftRightDistance, 0f, 0f);
        }
        if (RightTransform.position.x < this.gameObject.transform.position.x)
        {
            this.gameObject.transform.position += new Vector3(-_LeftRightDistance, 0f, 0f);
        }

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, mainCamera.transform.position.y, this.gameObject.transform.position.z);
    }
}
