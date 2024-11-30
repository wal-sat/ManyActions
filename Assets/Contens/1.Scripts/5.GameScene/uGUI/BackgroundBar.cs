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
        this.gameObject.transform.position += new Vector3(-1 * SPEED * Time.deltaTime, 0f ,0f); 

        if (this.gameObject.transform.position.x < EndPosition.position.x)
        {
            this.gameObject.transform.position = StartPosition.position;
        }   
    }
}
