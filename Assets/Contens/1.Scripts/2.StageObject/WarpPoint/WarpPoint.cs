using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    [SerializeField] WarpPointManager warpPointManager;
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] WarpPointView warpPointView;
    [SerializeField] GameObject Player;
    [SerializeField] WarpPoint warpPoint;
    
    [SerializeField] float COOL_TIME;

    private float _timer;
    private bool _onTimer;
    private bool _canWarp;
    private Vector2 _warpPointPosition;

    private void Awake()
    {
        _canWarp = true;
        _warpPointPosition = warpPoint.gameObject.transform.position;

        warpPointManager.Register(this);
        stageObjectCollisionArea.triggerEnter = TriggerEnter;
    }
    private void FixedUpdate()
    {
        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_TIME)
            {
                _onTimer = false;
                _canWarp = true;
                warpPointView.EnableView(true);
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

            warpPointView.EnableView(false);

            StartCoroutine(CWarp());

            S_SEManager._instance.Play("s_warpPoint");
        }
    }

    IEnumerator CWarp()
    {
        warpPoint.Warped();
        
        yield return null;

        Player.transform.position = new Vector3(_warpPointPosition.x, _warpPointPosition.y, Player.transform.position.z);
    }

    public void Warped()
    {
        _timer = 0;
        _onTimer = true;
        _canWarp = false;

        warpPointView.EnableView(false);
    }

    public void Initialize()
    {
        StopAllCoroutines();

        _onTimer = false;
        _canWarp = true;
        warpPointView.EnableView(true);
    }
}
