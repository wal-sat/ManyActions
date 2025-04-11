using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageSelectCameraMovement : MonoBehaviour
{
    [SerializeField] GameObject FollowedObject;
    [SerializeField] GameObject NormalEnvironment;
    [SerializeField] GameObject ReverseEnvironment;

    private const float XDistance = 24f;
    private const float YDistance = -14f;

    public void SetCameraPosition(int stageIndex, int undergroundIndex)
    {
        Vector3 movePosition = new Vector3(XDistance * stageIndex, YDistance * undergroundIndex, FollowedObject.transform.position.z);
        FollowedObject.transform.DOMove(movePosition, 1.1f).SetEase(Ease.OutCubic);
    }

    public void SetEnviroment(int reverseIndex)
    {
        if (reverseIndex == 0) {
            NormalEnvironment.SetActive(true);
            ReverseEnvironment.SetActive(false);
        } else {
            NormalEnvironment.SetActive(false);
            ReverseEnvironment.SetActive(true);
        }
    }
}
