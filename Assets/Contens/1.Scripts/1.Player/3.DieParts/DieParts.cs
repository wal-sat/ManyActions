using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class DieParts : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private int _destroyDistanceX;
    private int _destroyDistanceY;
    private Action<GameObject> onDestroyCallBack;

    public void Initialize(int destroyDistanceX, int destroyDistanceY, Action<GameObject> onDestroy)
    {
        _destroyDistanceX = destroyDistanceX;
        _destroyDistanceY = destroyDistanceY;
        onDestroyCallBack = onDestroy;
    }

    private void Start() 
    {
        StartCoroutine( CInvokeRealtime( () => CheckDestroyDistance() ) );
    }

    private void CheckDestroyDistance()
    {
        Vector2 distance = mainCamera.transform.position - this.gameObject.transform.position;
        if ( Mathf.Abs(distance.x) > _destroyDistanceX || Mathf.Abs(distance.y) > _destroyDistanceY ) 
        {
            StopAllCoroutines();
            onDestroyCallBack(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator CInvokeRealtime(Action action)
    {
        yield return new WaitForSecondsRealtime(2.0f);
        if (action != null) action();
        StartCoroutine(CInvokeRealtime(action));
    }
}
