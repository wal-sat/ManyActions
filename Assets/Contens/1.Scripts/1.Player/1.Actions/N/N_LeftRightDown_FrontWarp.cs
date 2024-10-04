using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_LeftRightDown_FrontWarp : PlayerActionRequireCoolDownBase
{
    [SerializeField] N_Up_Warp n_Up_Warp;
    [SerializeField] GameObject Player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private float WARP_DISTANCE;

    private float _distance;

    public override void InitAction()
    {
        if (n_Up_Warp.isWarping)
        {
            return;
        }
        
        if (base.isCoolDowning) return;

        base.InitAction();

        _distance = WARP_DISTANCE;

        if (assignedInput == InputKind.N_Down)
        {
            Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y - _distance);
            rb.velocity = new Vector2(0f, 0f);

            return;
        }
        else if (assignedInput == InputKind.N_Left) _distance *= -1;

        Player.transform.position = new Vector2(Player.transform.position.x + _distance, Player.transform.position.y);
        rb.velocity = new Vector2(0f, 0f);
    }
}
