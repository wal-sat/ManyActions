using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] SavePoint _startPoint;
    [SerializeField] GameObject Player;
    [HideInInspector] public SavePoint savePoint;

    private void Start()
    {
        savePoint = _startPoint;
    }

    public void RegisterSavePoint(SavePoint newSavePoint)
    {
        if (savePoint.savePointIndex <= newSavePoint.savePointIndex)
        {
            savePoint = newSavePoint;
        }
    }

    public void TeleportStartPosition()
    {
        Player.transform.position = new Vector3(_startPoint.transform.position.x, _startPoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.EnableActions(_startPoint.stageActionData);
    }

    public void TeleportRestartPosition()
    {
        Player.transform.position = new Vector3(savePoint.transform.position.x, savePoint.transform.position.y, Player.transform.position.z);

        playerManager.PlayerActionManager.EnableActions(savePoint.stageActionData);
    }
}
