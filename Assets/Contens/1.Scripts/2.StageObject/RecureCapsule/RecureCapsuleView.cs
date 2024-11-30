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

    public void OnRecure(float coolTime)
    {
        StartCoroutine(CRecureEffect(coolTime));
    }

    IEnumerator CRecureEffect(float coolTime)
    {
        spriteRenderer.sprite = disableSprite;

        yield return new WaitForSeconds(coolTime);

        spriteRenderer.sprite = defaultSprite;
    }
}
