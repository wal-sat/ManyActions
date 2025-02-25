using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Button : MonoBehaviour
{
    [SerializeField] ButtonManager buttonManager;
    [SerializeField] ButtonView buttonView;
    [SerializeField] GameObject movedObject;
    [SerializeField] Vector2 movePoint;

    private const float MOVE_TIME = 1.5f;

    private Vector2 _defaultPosition;
    private bool _isEnable;
    private Tween _tween;

    private void Start()
    {
        buttonManager.Register(this);
        
        _defaultPosition = movedObject.transform.position;

        Init();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_isEnable)
            {
                _isEnable = false;

                buttonView.SpriteChange(false);
                Move();

                S_SEManager._instance.Play("s_button");
                S_SEManager._instance.Play("s_movable");
            }
        }
    }

    public void Init()
    {
        _isEnable = true;
        _tween.Kill();

        movedObject.transform.position = _defaultPosition;
        buttonView.SpriteChange(true);
    }

    private void Move()
    {
        Vector2 pos = new Vector2(_defaultPosition.x + movePoint.x, _defaultPosition.y + movePoint.y);
        _tween = movedObject.transform.DOMove(pos, MOVE_TIME).SetEase(Ease.OutQuad);
    }
}
