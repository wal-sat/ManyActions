using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutCameraDie : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject Player;
    [SerializeField] float DIE_TIME;

    private float _timer;

    public void OutCameraUpdate()
    {
        if (IsOutCamera())
        {
            _timer += Time.deltaTime;

            if (_timer > DIE_TIME)
            {
                _timer = 0;
                stageManager.Restart();
            }
        }
        else
        {
            _timer = 0;
        }
    }

    private bool IsOutCamera()
    {
        bool isOutCamera = true;

        // 画面のワールド座標の範囲を取得
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        // スポーン範囲を計算
        float minX = screenBottomLeft.x;
        float maxX = screenTopRight.x;
        float minY = screenBottomLeft.y;
        float maxY = screenTopRight.y;

        if (minX < Player.transform.position.x && Player.transform.position.x < maxX)
        {
            if (minY < Player.transform.position.y && Player.transform.position.y < maxY)
            {
                isOutCamera = false;
            }
        }

        return isOutCamera;
    }
}