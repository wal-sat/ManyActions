using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : AttackableBase
{
    public override void Die()
    {
        float angleZ = this.transform.rotation.eulerAngles.z;
        stageManager.Restart(angleZ);
    }
}
