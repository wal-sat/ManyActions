using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockManager : MonoBehaviour
{
    private List<BreakableBlock> blocks = new List<BreakableBlock>();

    public void Register(BreakableBlock block)
    {
        blocks.Add(block);
    }

    public void Initialize()
    {
        foreach (var block in blocks)
        {
            block.Initialize();
        }
    }
}
