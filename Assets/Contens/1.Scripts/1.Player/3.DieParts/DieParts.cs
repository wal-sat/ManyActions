using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class DieParts : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform TopLeftTransform;
    [SerializeField] Transform BottomRightTransform;
    [SerializeField] bool isFace;

    Action<GameObject> destroy;

    private void Update() 
    {
        if (isFace && IsOutStageArea())
        {
            StartCoroutine(CDestroy());
        }
    }

    public void Init(Action<GameObject> action)
    {
        destroy = action;

        if (!isFace)
        {
            //顔以外消えるようにする
            //StartCoroutine(CDestroy());
        }
    }

    IEnumerator CDestroy()
    {
        yield return new WaitForSeconds(1.5f);

        destroy(this.gameObject);
        Destroy(this.gameObject);
    }

    private bool IsOutStageArea()
    {
        bool isOutCamera = true;

        // 画面のワールド座標の範囲を取得
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));

        float minX = TopLeftTransform.position.x - (screenTopRight.x - screenBottomLeft.x) / 2;
        float maxX = BottomRightTransform.position.x + (screenTopRight.x - screenBottomLeft.x) / 2;
        float minY = BottomRightTransform.position.y - (screenTopRight.y - screenBottomLeft.y) / 2;
        float maxY = TopLeftTransform.position.y + (screenTopRight.y - screenBottomLeft.y) / 2;

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
