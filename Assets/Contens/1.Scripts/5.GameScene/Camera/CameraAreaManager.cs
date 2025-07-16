using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class CameraAreaManager : MonoBehaviour
{
    private GameObject[] _virtualCamera = new GameObject[2];
    private CinemachineVirtualCamera[] _cinemachineVirtualCamera = new CinemachineVirtualCamera[2];
    private CinemachineVirtualCamera _cinemachineVirtualCameraSleep;
    private LockAxisCamera[] _lockAxisCamera = new LockAxisCamera[2];
    private List<CameraArea> _cameraAreas = new List<CameraArea>();
    private CameraArea _currentCameraArea;

    private int _currentCamera;
    int currentCamera
    {
        get => _currentCamera;
        set
        {
            _currentCamera = value;
            if (_currentCamera >= _virtualCamera.Length) _currentCamera = 0;
        }
    }

    public void Initialize(GameObject[] mainCamera, GameObject sleepCamera)
    {
        for (int i = 0; i < _virtualCamera.Length; i++)
        {
            _virtualCamera[i] = mainCamera[i];
            _cinemachineVirtualCamera[i] = _virtualCamera[i].GetComponent<CinemachineVirtualCamera>();
            _lockAxisCamera[i] = _virtualCamera[i].GetComponent<LockAxisCamera>();
        }
        _cinemachineVirtualCameraSleep = sleepCamera.GetComponent<CinemachineVirtualCamera>();
        _currentCamera = 0;
    }

    public void Register(CameraArea cameraArea)
    {
        _cameraAreas.Add(cameraArea);
        CameraAreaAddChange(cameraArea);
    }
    public void Unregister(CameraArea cameraArea)
    {
        _cameraAreas.Remove(cameraArea);
        CameraAreaRemoveChange(cameraArea);
    }

    public void CameraAreaAddChange(CameraArea cameraArea)
    {
        cameraArea = _cameraAreas.OrderByDescending(item => item.cameraAreaIndex).FirstOrDefault();
        if (_currentCameraArea == null || _currentCameraArea != cameraArea)
        {
            _cinemachineVirtualCamera[currentCamera].Priority = cameraArea.cameraAreaIndex;
            _cinemachineVirtualCamera[currentCamera].m_Lens.OrthographicSize = cameraArea.cameraSize;
            _cinemachineVirtualCameraSleep.m_Lens.OrthographicSize = cameraArea.cameraSize;
            _lockAxisCamera[currentCamera].SetMoveRange(cameraArea.bottomLeftPos, cameraArea.topRightPos);

            _currentCameraArea = cameraArea;
            currentCamera++;
            _cinemachineVirtualCamera[currentCamera].Priority = -1;
        }
    }
    public void CameraAreaRemoveChange(CameraArea cameraArea)
    {
        if (_currentCameraArea != cameraArea) return;
        else
        {
            cameraArea = _cameraAreas.OrderByDescending(item => item.cameraAreaIndex).FirstOrDefault();
            if (cameraArea == null) return;
            
            _cinemachineVirtualCamera[currentCamera].Priority = cameraArea.cameraAreaIndex;
            _cinemachineVirtualCamera[currentCamera].m_Lens.OrthographicSize = cameraArea.cameraSize;
            _cinemachineVirtualCameraSleep.m_Lens.OrthographicSize = cameraArea.cameraSize;
            _lockAxisCamera[currentCamera].SetMoveRange(cameraArea.bottomLeftPos, cameraArea.topRightPos);

            _currentCameraArea = cameraArea;
            currentCamera++;
            _cinemachineVirtualCamera[currentCamera].Priority = -1;
        }
    }
}
