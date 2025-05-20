using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] StageObjectCollisionArea stageObjectCollisionArea;
    [SerializeField] GameObject Player;
    [SerializeField] float JUMP_POWER;

    private const float COOL_DOWN_TIME = 0.3f;
    private const float STAY_TIME = 0.5f;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerPreventStuck playerPreventStuck;
    private Vector2 _direction;
    private bool _isBoost;
    private bool _canBarrelBoost;

    private void Awake()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        playerPreventStuck = Player.GetComponent<PlayerPreventStuck>();

        stageObjectCollisionArea.triggerEnter = triggerEnter;

        float _angle = ( this.gameObject.transform.localEulerAngles.z + 360 ) % 360;
        float radians = _angle * Mathf.Deg2Rad;
        _direction = new Vector2(-1 * Mathf.Sin(radians), Mathf.Cos(radians));

        _canBarrelBoost = true;
    }

    private void FixedUpdate()
    {
        if (_isBoost)
        {
            if (playerMovement.IsLanding() && rb.velocity.y <= 0) Landing();
        }
    }

    private void Landing()
    {
        _isBoost = false;
        playerMovement.isBlownUpByBarrel = false;
    }

    private void triggerEnter()
    {
        if (_canBarrelBoost) StartCoroutine(CBarrel());

        _isBoost = true;
        _canBarrelBoost = false;
    }
    IEnumerator CBarrel()
    {
        playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, true);

        Player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Player.transform.position.z);
        Player.SetActive(false);

        playerMovement.isBlownUpByBarrel = true;

        yield return new WaitForSeconds(STAY_TIME);

        Player.SetActive(true);

        rb.velocity = new Vector3(_direction.x * JUMP_POWER, _direction.y * JUMP_POWER, 0);

        S_SEManager._instance.Play("s_barrel");

        yield return new WaitForSeconds(COOL_DOWN_TIME);

        _canBarrelBoost = true;

        playerPreventStuck.SetLockPreventStuckStatus(this.gameObject, false);
    }
}
