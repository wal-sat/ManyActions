using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    private float _timer;
    private int _index;

    public void ZInitialize()
    {
        _timer = 0;
        _index = 0;
        spriteRenderer.sprite = sprites[_index];

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
