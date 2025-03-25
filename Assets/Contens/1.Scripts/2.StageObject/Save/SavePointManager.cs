using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GearManager gearManager;
    [SerializeField] SavePoint _startPoint;
    [SerializeField] GameObject Player;
    [HideInInspector] public SavePoint savePoint;

    private void Awake()
    {
        savePoint = _startPoint;
    }

    public void RegisterSavePoint(SavePoint newSavePoint)
    {
        if (savePoint.savePointIndex <= newSavePoint.savePointIndex)
        {
            savePoint = newSavePoint;
        }

        gearManager.OnSave();
    }

    public void TeleportStartPosition()
    {
        Player.transform.position = new Vector3(_startPoint.transform.position.x, _startPoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.SetAvailableActions(_startPoint.acquireActionData);
    }

    public void TeleportRestartPosition()
    {
        Player.transform.position = new Vector3(savePoint.transform.position.x, savePoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.SetAvailableActions(savePoint.acquireActionData);
    }
}
