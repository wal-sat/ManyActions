using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCassetteManager : MonoBehaviour
{
    private List<ActionCassette> _actionCassettes = new List<ActionCassette>();
    private List<ActionCassetteMinus> _actionCassettesMinus = new List<ActionCassetteMinus>();

    public void Register(ActionCassette actionCassette)
    {
        _actionCassettes.Add(actionCassette);
    }
    public void RegisterMinus(ActionCassetteMinus actionCassetteMinus)
    {
        _actionCassettesMinus.Add(actionCassetteMinus);
    }

    public void Initialize()
    {
        foreach (var actionCassette in _actionCassettes)
        {
            actionCassette.Initialize();
        }
        foreach (var actionCassetteMinus in _actionCassettesMinus)
        {
            actionCassetteMinus.Initialize();
        }
    }
}
