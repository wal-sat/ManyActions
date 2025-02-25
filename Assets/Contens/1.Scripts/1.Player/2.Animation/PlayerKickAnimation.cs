using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerKickAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    [HideInInspector] public bool animationStart;

    private float _timer;
    private int _index;

    [Button]
    public void AnimationStart()
    {
        _timer = 0;
        _index = 1;
        spriteRenderer.sprite = sprites[_index];

        animationStart = true;
    }

    private void FixedUpdate()
    {
        if (!animationStart) return;

        _timer += Time.deltaTime;

        if (_timer >= ANIMATION_TIME)
        {
            _timer = 0;
            _index ++;

            if (_index >= sprites.Length)
            {
                _timer = 0;
                _index = 0;
                spriteRenderer.sprite = sprites[_index];

                animationStart = false;
                return;
            } 

            spriteRenderer.sprite = sprites[_index];
        }
    }
}
