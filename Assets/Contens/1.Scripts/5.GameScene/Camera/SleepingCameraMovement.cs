using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SleepingCameraMovement : MonoBehaviour
{
    [SerializeField] float SPEED;

    private GameObject _sleepCamera;
    private LockAxisCamera _lockAxisCamera;

    public void Initialize(GameObject sleepCamera)
    {
        _sleepCamera = sleepCamera;
        _lockAxisCamera = _sleepCamera.GetComponent<LockAxisCamera>();
    }

    public void SleepCameraInit(Vector2 initPosition, Vector2 bottomLeftPos, Vector2 topRightPos)
    {
        _sleepCamera.transform.position = initPosition;
        _lockAxisCamera.SetMoveRange(bottomLeftPos, topRightPos);
    }
    public void SleepCameraMove(Vector2 direction)
    {
        _sleepCamera.transform.position = new Vector2(_sleepCamera.transform.position.x + direction.x * SPEED * Time.deltaTime, 
                                                     _sleepCamera.transform.position.y + direction.y * SPEED * Time.deltaTime);
    }
}
