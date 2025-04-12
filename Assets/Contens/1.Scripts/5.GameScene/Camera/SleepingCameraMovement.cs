using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SleepingCameraMovement : MonoBehaviour
{
    [SerializeField] GameObject sleepCamera;
    [SerializeField] GameObject followedObject;
    [SerializeField] float SPEED;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private LockAxisCamera _lockAxisCamera;

    private Vector2 _bottomLeftPos;
    private Vector2 _topRightPos;



    private void Awake()
    {
        _cinemachineVirtualCamera = sleepCamera.GetComponent<CinemachineVirtualCamera>();
        _lockAxisCamera = sleepCamera.GetComponent<LockAxisCamera>();
    }

    public void SleepCameraInit(Vector2 initPosition, float cameraSize, Vector2 bottomLeftPos, Vector2 topRightPos)
    {
        followedObject.transform.position = initPosition;
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = cameraSize;
        _lockAxisCamera.SetMoveRange(bottomLeftPos, topRightPos);

        _bottomLeftPos = bottomLeftPos;
        _topRightPos = topRightPos;
    }
    public void SleepCameraMove(Vector2 direction)
    {
        followedObject.transform.position = new Vector2(followedObject.transform.position.x + direction.x * SPEED * Time.deltaTime, followedObject.transform.position.y + direction.y * SPEED * Time.deltaTime);

        if (followedObject.transform.position.x < _bottomLeftPos.x) followedObject.transform.position = new Vector3(_bottomLeftPos.x, followedObject.transform.position.y, followedObject.transform.position.z);
        else if (_topRightPos.x < followedObject.transform.position.x) followedObject.transform.position = new Vector3(_topRightPos.x, followedObject.transform.position.y, followedObject.transform.position.z);

        if (followedObject.transform.position.y < _bottomLeftPos.y) followedObject.transform.position = new Vector3(followedObject.transform.position.x, _bottomLeftPos.y, followedObject.transform.position.z);
        else if (_topRightPos.y < followedObject.transform.position.y) followedObject.transform.position = new Vector3(followedObject.transform.position.x, _topRightPos.y, followedObject.transform.position.z);

    }
}
