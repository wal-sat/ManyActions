using UnityEngine;
using Cinemachine;
using System.Diagnostics.SymbolStore;

#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockAxisCamera : CinemachineExtension 
{
    [SerializeField] private int lockPositionZ;
    [SerializeField] private Vector2 bottomLeftPos;
    [SerializeField] private Vector2 topRightPos;
    
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;

            // カメラのサイズを取得
            float camHalfHeight = vcam.State.Lens.OrthographicSize;
            float camHalfWidth = camHalfHeight * vcam.State.Lens.Aspect;

            float camWidth = camHalfWidth * 2f;
            float camHeight = camHalfHeight * 2f;

            // 指定された範囲のサイズ
            float regionWidth = topRightPos.x - bottomLeftPos.x;
            float regionHeight = topRightPos.y - bottomLeftPos.y;

            newPos.z = lockPositionZ;

            // 横幅が十分な場合：制限する
            if (regionWidth >= camWidth)
            {
                float minX = bottomLeftPos.x + camHalfWidth;
                float maxX = topRightPos.x - camHalfWidth;
                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            }
            else
            {
                // カメラの左端を bottomLeft に合わせる（右にはみ出してOK）
                newPos.x = bottomLeftPos.x + camHalfWidth;
            }

            // 高さが十分な場合：制限する
            if (regionHeight >= camHeight)
            {
                float minY = bottomLeftPos.y + camHalfHeight;
                float maxY = topRightPos.y - camHalfHeight;
                newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
            }
            else
            {
                // カメラの下端を bottomLeft に合わせる（上にはみ出してOK）
                newPos.y = bottomLeftPos.y + camHalfHeight;
            }

            state.RawPosition = newPos;
            this.transform.position = newPos;
        }
    }

    public void SetMoveRange(Vector2 bottomLeft, Vector2 topRight)
    {
        bottomLeftPos = bottomLeft;
        topRightPos = topRight;
    }
}

