using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SleepingCameraMovement : MonoBehaviour
{
    [SerializeField] GameObject sleepCamera;
    [SerializeField] float SPEED;

    private LockAxisCamera _lockAxisCamera;

    private void Awake()
    {
        _lockAxisCamera = sleepCamera.GetComponent<LockAxisCamera>();
    }

    public void SleepCameraInit(Vector2 initPosition, Vector2 bottomLeftPos, Vector2 topRightPos)
    {
        sleepCamera.transform.position = initPosition;
        _lockAxisCamera.SetMoveRange(bottomLeftPos, topRightPos);
    }
    public void SleepCameraMove(Vector2 direction)
    {
        sleepCamera.transform.position = new Vector2(sleepCamera.transform.position.x + direction.x * SPEED * Time.deltaTime, 
                                                     sleepCamera.transform.position.y + direction.y * SPEED * Time.deltaTime);
    }
}
