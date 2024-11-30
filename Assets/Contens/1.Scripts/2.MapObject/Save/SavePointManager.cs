using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    [SerializeField] SavePoint _startPoint;
    [SerializeField] GameObject Player;
    private SavePoint _savePoint;

    private void Start()
    {
        _savePoint = _startPoint;
    }

    public void RegisterSavePoint(SavePoint newSavePoint)
    {
        if (_savePoint.savePointIndex <= newSavePoint.savePointIndex)
        {
            _savePoint = newSavePoint;
        }
    }

    public void TeleportStartPosition()
    {
        Player.transform.position = new Vector3(_startPoint.transform.position.x, _startPoint.transform.position.y, Player.transform.position.z);
    }

    public bool TeleportRestartPosition()
    {
        Player.transform.position = new Vector3(_savePoint.transform.position.x, _savePoint.transform.position.y, Player.transform.position.z);

        return _savePoint.facingRight;
    }
}
