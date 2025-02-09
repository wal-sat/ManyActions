using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingCameraMovement : MonoBehaviour
{
    [SerializeField] GameSceneAnyKeyInput gameSceneAnyKeyInput;
    [SerializeField] GameObject followedObject;
    [SerializeField] float SPEED;

    private Vector2 _initPosition;
    private float _maxMinusMove;
    private float _maxPlusMove;

    public void SleepCameraInit(Vector2 initPosition, Vector2 moveDistance)
    {
        _initPosition = initPosition;
        if (_initPosition.x < 0) _initPosition = new Vector2(0, _initPosition.y);
        _maxMinusMove = moveDistance.x;
        _maxPlusMove = moveDistance.y;

        followedObject.transform.position = _initPosition;
    }
    public void SleepCameraMove(Vector2 direction)
    {
        followedObject.transform.position = new Vector2(followedObject.transform.position.x + direction.x * SPEED * Time.deltaTime, 0);

        if (followedObject.transform.position.x < _initPosition.x - _maxMinusMove)
        {
            followedObject.transform.position = new Vector2(_initPosition.x - _maxMinusMove, followedObject.transform.position.y);
        }
        if (followedObject.transform.position.x > _initPosition.x + _maxPlusMove) 
        {
            followedObject.transform.position = new Vector2(_initPosition.x + _maxPlusMove, followedObject.transform.position.y);
        }
    }
}
