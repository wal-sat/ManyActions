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
            newPos.z = lockPositionZ;

            if (newPos.x < bottomLeftPos.x) newPos.x = bottomLeftPos.x;
            else if (topRightPos.x < newPos.x) newPos.x = topRightPos.x;

            if (newPos.y < bottomLeftPos.y) newPos.y = bottomLeftPos.y;
            else if (topRightPos.y < newPos.y) newPos.y = topRightPos.y;

            state.RawPosition = newPos;
        }
    }

    public void SetMoveRange(Vector2 bottomLeft, Vector2 topRight)
    {
        bottomLeftPos = bottomLeft;
        topRightPos = topRight;
    }
}

