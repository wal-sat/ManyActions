using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    private float _timer;
    private int _index;

    public void SleepInitialize()
    {
        _timer = 0;
        _index = 0;
        spriteRenderer.sprite = sprites[_index];
    }
    public void SleepEnd()
    {
        spriteRenderer.sprite = defaultSprite;
    }

    //PlayerAnimationからFixUpdate()で呼ばれる
    public void SleepUpdate()
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
