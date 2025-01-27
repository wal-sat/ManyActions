using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPointEffect : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    private float _timer;
    private int _index;

    private void Start()
    {
        _timer = 0;
        _index = 0;
        spriteRenderer.sprite = sprites[_index];
    }

    private void FixedUpdate()
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
