using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Crouching : PlayerActionBase
{
    //TODO:マジックナンバーを消す。
    //TODO:スプライトの変更を別のスクリプトで行う
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider2D;
    [SerializeField] Sprite main;
    [SerializeField] Sprite crouch;

    public override void InitAction()
    {
        spriteRenderer.sprite = crouch;
        capsuleCollider2D.offset = new Vector2(0, -0.175f);
        capsuleCollider2D.size = new Vector2(0.4f, 0.65f);
    }
    public override void InAction()
    {
        
    }
    public override void EndAction()
    {
        spriteRenderer.sprite = main;
        capsuleCollider2D.offset = new Vector2(0, -0.05f);
        capsuleCollider2D.size = new Vector2(0.4f, 0.9f);
    }
}
