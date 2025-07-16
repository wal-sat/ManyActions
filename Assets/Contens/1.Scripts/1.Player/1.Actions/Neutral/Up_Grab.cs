using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Grab : PlayerActionBase
{
    [SerializeField] GameObject Player;
    [SerializeField] RopeManager ropeManager;
    [SerializeField] Sprite main;
    [SerializeField] Sprite grab;

    private Rigidbody2D _rb;
    private PlayerMovement _playerMovement;
    private PlayerPreventStuck _playerPreventStuck;
    private SpriteRenderer _spriteRenderer;
    private Transform _ropeTransform;
    private Vector2 _ropePositionPast;
    private float _gravityScale;
    private bool _wasGrabRope;

    private void Awake()
    {
        _rb = Player.GetComponent<Rigidbody2D>();
        _playerMovement = Player.GetComponent<PlayerMovement>();
        _playerPreventStuck = Player.GetComponent<PlayerPreventStuck>();
        _spriteRenderer = Player.GetComponent<SpriteRenderer>();

        _gravityScale = _rb.gravityScale;
    }

    public override void InitAction()
    {
        base.InitAction();
        _spriteRenderer.sprite = grab;

        if (ropeManager.IsOverlapRope()) InitGrab();
    }
    public override void InAction()
    {
        _spriteRenderer.sprite = grab;

        if (ropeManager.IsOverlapRope() && !_wasGrabRope) InitGrab();
        else if (ropeManager.IsOverlapRope() && _wasGrabRope) InGrab();
        else CancelGrab();
    }
    public override void EndAction()
    {
        base.EndAction();
        _spriteRenderer.sprite = main;

        if (_wasGrabRope) CancelGrab();
    }
    public override void Initialize()
    {
        base.Initialize();
        _spriteRenderer.sprite = main;

        CancelGrab();
    }

    private void InitGrab()
    {
        if (!_wasGrabRope) S_SEManager._instance.Play("s_grabRope");
        _wasGrabRope = true;

        _rb.gravityScale = 0;
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _ropeTransform = ropeManager.GetRopeTransform();
        _ropePositionPast = _ropeTransform.position;
        Player.transform.position = new Vector3(_ropeTransform.position.x, Player.transform.position.y, Player.transform.position.z);

        _playerMovement.SetLockMovingStatus(this.gameObject, true);
        _playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, true);
    }
    private void InGrab()
    {
        Vector2 diffrentPosition = (Vector2) _ropeTransform.position - _ropePositionPast;
        Player.transform.position += new Vector3(diffrentPosition.x, diffrentPosition.y, 0f);

        _ropePositionPast = _ropeTransform.position;
    }
    private void CancelGrab()
    {
        _wasGrabRope = false;

        _rb.gravityScale = _gravityScale;

        _playerMovement.SetLockMovingStatus(this.gameObject, false);
        _playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, false);
    }
}
