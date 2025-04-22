using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public enum CameraKind { main, sleep }

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject[] mainCamera;
    [SerializeField] GameObject sleepCamera;
    [SerializeField] CameraAreaManager cameraAreaManager;
    [SerializeField] public CameraBodyChanger cameraBodyChanger;
    [SerializeField] public SleepingCameraMovement sleepingCameraMovement;

    private void Awake()
    {
        ChangeCamera(CameraKind.main);
        cameraAreaManager.Initialize(mainCamera, sleepCamera);
        cameraBodyChanger.Initialize(mainCamera);
        sleepingCameraMovement.Initialize(sleepCamera);
    }

    public void ChangeCamera(CameraKind cameraKind)
    {
        if (cameraKind == CameraKind.main)
        {
            sleepCamera.SetActive(false);
        }
        else if (cameraKind == CameraKind.sleep)
        {
            sleepCamera.SetActive(true);
        }
    }
}
