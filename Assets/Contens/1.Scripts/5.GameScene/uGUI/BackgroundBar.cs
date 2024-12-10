using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackgroundBar : MonoBehaviour
{
    [SerializeField] Transform StartPosition;
    [SerializeField] Transform EndPosition;
    [SerializeField] float SPEED;

    private void FixedUpdate()
    {
        this.gameObject.transform.localPosition += new Vector3(SPEED * Time.deltaTime, 0f ,0f); 

        if (this.gameObject.transform.localPosition.x > EndPosition.localPosition.x)
        {
            this.gameObject.transform.localPosition = StartPosition.localPosition;
        }   
    }
}
