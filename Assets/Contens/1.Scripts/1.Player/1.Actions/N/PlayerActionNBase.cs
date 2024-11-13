using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerActionNBase : PlayerActionRequireCoolDownBase
{
    [SerializeField] public PlayerActionManager playerActionManager;
    [SerializeField] public PlayerActionNManager playerActionNManager;
}
