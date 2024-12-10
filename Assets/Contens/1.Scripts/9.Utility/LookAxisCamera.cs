using UnityEngine;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockAxisCamera : CinemachineExtension 
{
    [SerializeField] private bool x_islocked;
    [SerializeField] private bool y_islocked;
    [SerializeField] private bool z_islocked;
    [SerializeField] private Vector3 lockPosition;
    [SerializeField] private Transform topLeftTransform;
    [SerializeField] private Transform bottomRightTransform;
    
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var newPos = state.RawPosition;
            if (x_islocked) newPos.x = lockPosition.x;
            if (y_islocked) newPos.y = lockPosition.y;
            if (z_islocked) newPos.z = lockPosition.z;

            if (topLeftTransform != null)
            {
                if (newPos.x <= topLeftTransform.position.x) newPos.x = topLeftTransform.position.x; 
                if (bottomRightTransform.position.x <= newPos.x) newPos.x = bottomRightTransform.position.x; 
            }
            if (bottomRightTransform != null)
            {
                if (newPos.y <= bottomRightTransform.position.y) newPos.y = bottomRightTransform.position.y; 
                if (topLeftTransform.position.y <= newPos.y) newPos.y = topLeftTransform.position.y;
            }
            state.RawPosition = newPos;
        }
    }
}

