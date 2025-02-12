using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecureCapsuleView : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite disableSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultSprite;
    }

    public void EnableView(bool isEnable)
    {
        if (isEnable) spriteRenderer.sprite = defaultSprite;
        else spriteRenderer.sprite = disableSprite;
    }
}
