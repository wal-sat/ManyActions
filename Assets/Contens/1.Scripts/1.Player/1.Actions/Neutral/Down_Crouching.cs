using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Crouching : PlayerActionBase
{
    //TODO:スプライトの変更を別のスクリプトで行う
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject OverheadChecher;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider2D;
    [SerializeField] Sprite main;
    [SerializeField] Sprite crouch;

    private Vector2 DEFAULT_OFFSET = new Vector2(0, -0.05f);
    private Vector2 DEFAULT_SIZE = new Vector2(0.4f, 0.9f);
    private Vector2 DEFAULT_OVERHEAD_POSITION = new Vector2(0, 0.45f);
    private Vector2 CROUCHING_OFFSET = new Vector2(0, -0.175f);
    private Vector2 CROUCHING_SIZE = new Vector2(0.4f, 0.65f);
    private Vector2 CROUCHING_OVERHEAD_POSITION = new Vector2(0, 0.325f);

    private float _defaultCapsuleSizeY;
    private const float CROUCHING_CAPSULE_SIZE_Y = 0.15f;

    private void Awake()
    {
        _defaultCapsuleSizeY = playerMovement.swapCapsuleSizeY;
    }

    public override void InitAction()
    {
        playerMovement.swapCapsuleSizeY = CROUCHING_CAPSULE_SIZE_Y;
        OverheadChecher.transform.localPosition = CROUCHING_OVERHEAD_POSITION;
        spriteRenderer.sprite = crouch;
        capsuleCollider2D.offset = CROUCHING_OFFSET;
        capsuleCollider2D.size = CROUCHING_SIZE;
    }
    public override void InAction()
    {
        
    }
    public override void EndAction()
    {
        playerMovement.swapCapsuleSizeY = _defaultCapsuleSizeY;
        OverheadChecher.transform.localPosition = DEFAULT_OVERHEAD_POSITION;
        spriteRenderer.sprite = main;
        capsuleCollider2D.offset = DEFAULT_OFFSET;
        capsuleCollider2D.size = DEFAULT_SIZE;
    }
    public override void Initialize()
    {
        playerMovement.swapCapsuleSizeY = _defaultCapsuleSizeY;
        OverheadChecher.transform.localPosition = DEFAULT_OVERHEAD_POSITION;
        spriteRenderer.sprite = main;
        capsuleCollider2D.offset = DEFAULT_OFFSET;
        capsuleCollider2D.size = DEFAULT_SIZE;
    }
}
