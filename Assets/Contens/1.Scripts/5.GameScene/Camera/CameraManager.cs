using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public enum CameraKind { main, sleep }

public class CameraManager : MonoBehaviour
{
    [SerializeField] public GameObject[] mainCamera;
    [SerializeField] GameObject sleepCamera;
    [SerializeField] public SleepingCameraMovement sleepingCameraMovement;

    private void Awake()
    {
        ChangeCamera(CameraKind.main);
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
