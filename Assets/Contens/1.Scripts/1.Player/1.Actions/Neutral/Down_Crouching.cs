using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Crouching : PlayerActionBase
{
    [SerializeField] Transform Player;
    private float _scaleY;

    public override void InitAction()
    {
        _scaleY = Player.localScale.y;
        Player.localScale = new Vector3(Player.localScale.x, Player.localScale.y / 2, 1f);
    }
    public override void InAction()
    {
        
    }
    public override void EndAction()
    {
        Player.localScale = new Vector3(Player.localScale.x, _scaleY, 1f);
    }
}
