using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] SavePointManager savePointManager;

    public void Restart()
    {
        bool facingRight = savePointManager.TeleportRestartPosition();
        
        playerManager.Initialize(facingRight);
        //爆発
        //フェード
        //開始処理
    }
}
