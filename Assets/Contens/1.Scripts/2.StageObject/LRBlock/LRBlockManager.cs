using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRBlockManager : MonoBehaviour
{
    [SerializeField] public float DURATION;

    private List<LRBlock> LRBlocks = new List<LRBlock>();
    private InputKind _currentInputKind;

    private void Start()
    {
        _currentInputKind = InputKind.L2;
    }

    public void Register(LRBlock lrBlock)
    {
        LRBlocks.Add(lrBlock);
    }

    public void LRBlockMove(InputKind inputKind)
    {
        if (_currentInputKind == inputKind) return;
        _currentInputKind = inputKind;

        foreach (var lrBlock in LRBlocks)
        {
            lrBlock.PlayAnimate(_currentInputKind);
        }
    }

    public void Initialize()
    {
        _currentInputKind = InputKind.L2;
        foreach (var lrBlock in LRBlocks)
        {
            lrBlock.Initialize();
        }
    }
}
