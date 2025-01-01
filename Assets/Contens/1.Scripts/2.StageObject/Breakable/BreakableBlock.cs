using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] BreakableBlockManager breakableBlockManager;
    [SerializeField] BreakableBlockView breakableBlockView;
    [SerializeField] Collider2D col;
    [SerializeField] Transform PlayerLandingChecker;
    [SerializeField] float BUFFER_TIME;

    private const float OFFSET = 0.2f;

    private bool _isEnable;
    private bool _isTimer;
    private float _timer;

    private void Awake()
    {
        breakableBlockManager.Register(this);

        Initialize();
    }

    private void FixedUpdate()
    {
        if (_isTimer)
        {
            _timer += Time.deltaTime;
            if (_timer > BUFFER_TIME)
            {
                _isTimer = false;
                _timer = 0;

                Break();
            }
        }
    }

    public void Initialize()
    {
        _isEnable = true;
        col.enabled = true;

        breakableBlockView.SpriteChange(true);
    }
    private void Break()
    {
        _isEnable = false;
        col.enabled = false;

        breakableBlockView.SpriteChange(false);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y + this.gameObject.transform.localScale.y / 2 - OFFSET <= PlayerLandingChecker.position.y) 
            {
                if (_isEnable)
                {
                    _isTimer = true;
                    _timer = 0;
                }
            }
        }
    }
}
