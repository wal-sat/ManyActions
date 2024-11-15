using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] private float ANIMATION_TIME;

    private float _timer;
    private int _index;

    private void FixedUpdate()
    {
        // いづれどこかからこれを呼び出す
        AnimationUpdate();
    }

    public void AnimationUpdate()
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
