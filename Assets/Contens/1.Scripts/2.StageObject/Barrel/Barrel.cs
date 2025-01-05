using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GameObject Player;
    [SerializeField] PlayerActionManager playerActionManager;
    [SerializeField] float JUMP_POWER;

    private const float COOL_DOWN_TIME = 0.1f;
    private const float STAY_TIME = 0.5f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerPreventStuck playerPreventStuck;
    private float _angle;
    private bool _isBoost;
    private bool _canBarrelBoost;
    private float _timer;
    private bool _onTimer;

    private void Awake()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        playerPreventStuck = Player.GetComponent<PlayerPreventStuck>();

        stageObjectCollisionArea.triggerEnter = triggerEnter;

        _angle = ( this.gameObject.transform.localEulerAngles.z + 360 ) % 360;
        _canBarrelBoost = true;
    }

    private void FixedUpdate()
    {
        if (_isBoost)
        {
            if (playerMovement.IsLanding() && rb.velocity.y <= 0) Landing();
        }

        if (_onTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > COOL_DOWN_TIME)
            {
                _onTimer = false;
                _canBarrelBoost = true;
            }
        }
    }

    private void Landing()
    {
        _isBoost = false;
        playerMovement.isLockMoving = false;
    }

    private void triggerEnter()
    {
        if (_canBarrelBoost) StartCoroutine(CBarrel());

        _isBoost = true;
        _canBarrelBoost = false;
    }
    IEnumerator CBarrel()
    {
        playerActionManager.Initialize();

        playerPreventStuck.isPreventingStuck = false;

        S_InputSystem._instance.canInput = false;

        Player.transform.position = this.transform.position;
        Player.SetActive(false);

        playerMovement.isLockMoving = true;

        yield return new WaitForSeconds(STAY_TIME);

        Player.SetActive(true);
        S_InputSystem._instance.canInput = true;

        float radians = _angle * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(-1 * Mathf.Sin(radians), Mathf.Cos(radians));
        rb.AddForce(forceDirection * JUMP_POWER, ForceMode2D.Impulse);

        _onTimer = true;
        _timer = 0;

        playerPreventStuck.isPreventingStuck = true;
    }
}
