using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRBlockView : MonoBehaviour
{
    [SerializeField] Sprite LSprite;
    [SerializeField] Sprite RSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = LSprite;
    }

    public void SpriteChange(InputKind inputKind)
    {
        if (inputKind == InputKind.L2) spriteRenderer.sprite = LSprite;
        else spriteRenderer.sprite = RSprite;
    }
}
