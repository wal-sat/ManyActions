using System;
using UnityEngine;

public class PlayerActionBase : MonoBehaviour
{
    [SerializeField] public InputKind assignedInput;
    [HideInInspector] public bool isEnable = true;

    public virtual void InitAction()
    {
        ;
    }
    public virtual void InAction()
    {
        ;
    }
    public virtual void EndAction()
    {
        ;
    }
}
