using System;
using UnityEngine;

public class PlayerActionBase : MonoBehaviour
{
    [SerializeField] public ActionKind actionKind;
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
    public virtual void Initialize()
    {
        ;
    }
}
