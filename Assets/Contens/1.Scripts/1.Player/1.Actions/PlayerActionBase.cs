using System;
using UnityEngine;

public class PlayerActionBase : MonoBehaviour
{
    [SerializeField] public ActionKind actionKind;
    [SerializeField] public InputKind assignedInput;
    [HideInInspector] public bool isEnable = true;
    [HideInInspector] public bool isAction = false;
    [HideInInspector] public bool isCoolTime = false;

    public virtual void InitAction()
    {
        isAction = true;
    }
    public virtual void InAction()
    {
        ;
    }
    public virtual void EndAction()
    {
        isAction = false;
    }
    public virtual void Initialize()
    {
        isAction = false;
    }
    public virtual void SwapInAction()
    {
        ;
    }
}
