using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public enum CameraKind { main, sleep }

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject sleepCamera;
    [SerializeField] public SleepingCameraMovement sleepingCameraMovement;

    private Dictionary<CameraKind, GameObject> cameras = new Dictionary<CameraKind, GameObject>();

    private void Awake()
    {
        cameras.Add(CameraKind.main, mainCamera);
        cameras.Add(CameraKind.sleep, sleepCamera);

        ChangeCamera(CameraKind.main);
    }

    public void ChangeCamera(CameraKind cameraKind)
    {
        foreach (KeyValuePair<CameraKind, GameObject> camera in cameras)
        {
            camera.Value.SetActive(false);
        }

        cameras[cameraKind].SetActive(true);
    }
}
