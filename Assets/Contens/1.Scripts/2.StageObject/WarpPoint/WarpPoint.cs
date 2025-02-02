using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] WarpPointView warpPointView;
    [SerializeField] GameObject Player;
    [SerializeField] WarpPoint warpPoint;
    
    [SerializeField] float COOL_TIME;

    private float _timer;
    private bool _onTimer;
    private bool _canWarp;
    private Vector2 _warpPoint;

    private void Awake()
    {
        _canWarp = true;
        _warpPoint = warpPoint.gameObject.transform.position;

        stageObjectCollisionArea.triggerEnter = TriggerEnter;
    }
    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_TIME)
            {
                _timer = 0;
                _onTimer = false;
                _canWarp = true;
            }
        }
    }

    private void TriggerEnter()
    {
        if (_canWarp)
        {
            _timer = 0;
            _onTimer = true;
            _canWarp = false;

            warpPointView.OnRecure(COOL_TIME);

            StartCoroutine(CWarp());

            S_SEManager._instance.Play("s_warpPoint");
        }
    }

    IEnumerator CWarp()
    {
        warpPoint.Warped();
        
        yield return null;

        Player.transform.position = new Vector3(_warpPoint.x, _warpPoint.y, Player.transform.position.z);
    }

    public void Warped()
    {
        _onTimer = true;
        _canWarp = false;
        warpPointView.OnRecure(COOL_TIME);
    }
}
