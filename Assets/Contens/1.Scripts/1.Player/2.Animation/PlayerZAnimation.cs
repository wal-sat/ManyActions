using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    private float _positionX;
    private float _scaleX;
    private float _timer;
    private int _index;

    private void Awake()
    {
        _positionX = this.transform.localPosition.x;
        _scaleX = this.transform.localScale.x;
    }

    public void ZInitialize(bool isFacingRight)
    {
        _timer = 0;
        _index = 0;
        spriteRenderer.sprite = sprites[_index];

        if (isFacingRight) 
        {
            this.transform.localPosition = new Vector3(_positionX, this.transform.localPosition.y, this.transform.localPosition.z);
            this.transform.localScale = new Vector3(_scaleX, this.transform.localScale.y, this.transform.localScale.z);
        }
        else 
        {
            this.transform.localPosition = new Vector3(-_positionX, this.transform.localPosition.y, this.transform.localPosition.z);
            this.transform.localScale = new Vector3(-_scaleX, this.transform.localScale.y, this.transform.localScale.z);
        }

        this.gameObject.SetActive(true);
    }
    public void ZEnd()
    {
        this.gameObject.SetActive(false);
    }

    //PlayerAnimationからFixUpdate()で呼ばれる
    public void ZUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= ANIMATION_TIME)
        {
            _timer = 0;
            _index ++;

            if (_index >= sprites.Length) _index = 0;

            spriteRenderer.sprite = sprites[_index];
        }
    }
}
