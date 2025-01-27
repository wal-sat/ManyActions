using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKidouUI : MonoBehaviour
{
    private float _scaleX;
    private void Awake()
    {
        _scaleX = this.transform.localScale.x;
    }
    public void SetActiveTrue(bool isFacingRight)
    {
        if (isFacingRight) this.transform.localScale = new Vector3(_scaleX, this.transform.localScale.y, this.transform.localScale.z);
        else this.transform.localScale = new Vector3(-_scaleX, this.transform.localScale.y, this.transform.localScale.z);

        this.gameObject.SetActive(true);
    }
    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
