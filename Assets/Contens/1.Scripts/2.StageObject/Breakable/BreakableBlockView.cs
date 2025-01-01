using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockView : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite defaultSprite;

    private void Awake()
    {
        spriteRenderer.sprite = defaultSprite;
    }

    public void SpriteChange(bool isEnable)
    {
        if (isEnable) spriteRenderer.sprite = defaultSprite;
        else if (!isEnable) spriteRenderer.sprite = null;
    }
}
