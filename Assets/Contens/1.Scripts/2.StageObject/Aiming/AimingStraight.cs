using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AimingStraght : MonoBehaviour, IAiming
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform Player;
    [SerializeField] float SPEED;
    [SerializeField] float DIE_TIME;

    private bool _isTemplate = true;
    private float _timer;
    private Vector2 _direction;

    public void FixedUpdate()
    {
        if (IsOutCamera() && !_isTemplate)
        {
            _timer += Time.deltaTime;

            if (_timer > DIE_TIME)
            {
                _timer = 0;
                Destroy(this.gameObject);
            }
        }
        else
        {
            _timer = 0;
        }

        rb.velocity = _direction * SPEED * Time.deltaTime;
    }

    public void Init()
    {
        _direction = ( (Player.position - this.transform.position) * 10f ).normalized;
        _isTemplate = false;
    }

    private bool IsOutCamera()
    {
        bool isOutCamera = true;

        // 画面のワールド座標の範囲を取得
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        float minX = screenBottomLeft.x;
        float maxX = screenTopRight.x;
        float minY = screenBottomLeft.y;
        float maxY = screenTopRight.y;

        if (minX < this.transform.position.x && this.transform.position.x < maxX)
        {
            if (minY < this.transform.position.y && this.transform.position.y < maxY)
            {
                isOutCamera = false;
            }
        }

        return isOutCamera;
    }
}
