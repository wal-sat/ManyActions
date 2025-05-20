using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GearManager gearManager;
    [SerializeField] SavePoint _startPoint;
    [SerializeField] GameObject Player;
    [HideInInspector] public SavePoint savePoint { get; private set; }

    private void Awake()
    {
        savePoint = _startPoint;
    }

    public void RegisterSavePoint(SavePoint newSavePoint, bool isIgnoreIndex = false)
    {
        if (savePoint.savePointIndex <= newSavePoint.savePointIndex || isIgnoreIndex)
        {
            savePoint = newSavePoint;
        }

        gearManager.OnSave();
    }

    public void TeleportStartPosition(SavePoint startPoint)
    {
        RegisterSavePoint(startPoint, true);
        Player.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.SetAvailableActions(startPoint.acquireActionData);
    }

    public void TeleportRestartPosition()
    {
        Player.transform.position = new Vector3(savePoint.transform.position.x, savePoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.SetAvailableActions(savePoint.acquireActionData);
    }
}
