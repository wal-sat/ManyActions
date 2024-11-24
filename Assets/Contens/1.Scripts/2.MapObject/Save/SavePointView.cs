using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointView : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite glossSprite;
    [SerializeField] float GLOSS_TIME;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultSprite;
    }

    public void OnSave()
    {
        StartCoroutine(CSaveEffect());
    }

    IEnumerator CSaveEffect()
    {
        spriteRenderer.sprite = glossSprite;

        yield return new WaitForSeconds(GLOSS_TIME);

        spriteRenderer.sprite = defaultSprite;
    }
}
