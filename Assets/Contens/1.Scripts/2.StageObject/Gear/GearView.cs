using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearView : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite disableSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultSprite;
    }

    public void SpriteChange(bool isEnable)
    {
        if (isEnable) spriteRenderer.sprite = defaultSprite;
        else if (!isEnable) spriteRenderer.sprite = disableSprite;
    }
}
