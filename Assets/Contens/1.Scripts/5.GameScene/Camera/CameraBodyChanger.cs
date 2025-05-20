using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBodyChanger : MonoBehaviour
{
    [SerializeField] CinemachineBrain _cinemachineBrain;

    private GameObject[] _virtualCamera = new GameObject[2];
    private CinemachineFramingTransposer[] _cinemachineFramingTransposer = new CinemachineFramingTransposer[2];

    private float _lookaheadTime;
    private float _lookaheadSmoothing;
    private float _xDumping;
    private float _yDumping;
    private float _blendTime;

    public void Initialize(GameObject[] mainCamera)
    {
        for (int i = 0; i < _virtualCamera.Length; i++)
        {
            _virtualCamera[i] = mainCamera[i];
            _cinemachineFramingTransposer[i] = _virtualCamera[i].GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        _lookaheadSmoothing = _cinemachineFramingTransposer[0].m_LookaheadSmoothing;
        _lookaheadTime = _cinemachineFramingTransposer[0].m_LookaheadTime;
        _xDumping = _cinemachineFramingTransposer[0].m_XDamping;
        _yDumping = _cinemachineFramingTransposer[0].m_YDamping;
        _blendTime = _cinemachineBrain.m_DefaultBlend.m_Time;
    }

    public void EnableDumping(bool isDumping)
    {
        if (isDumping)
        {
            for (int i = 0; i < _cinemachineFramingTransposer.Length; i++)
            {
                _cinemachineFramingTransposer[i].m_LookaheadTime = _lookaheadTime;
                _cinemachineFramingTransposer[i].m_LookaheadSmoothing = _lookaheadSmoothing;
                _cinemachineFramingTransposer[i].m_XDamping = _xDumping;
                _cinemachineFramingTransposer[i].m_YDamping = _yDumping;
                _cinemachineBrain.m_DefaultBlend.m_Time = _blendTime;
            }
        }
        else
        {
            for (int i = 0; i < _cinemachineFramingTransposer.Length; i++)
            {
                _cinemachineFramingTransposer[i].m_LookaheadTime = 0.1f;
                _cinemachineFramingTransposer[i].m_LookaheadSmoothing = 0.1f;
                _cinemachineFramingTransposer[i].m_XDamping = 0f;
                _cinemachineFramingTransposer[i].m_YDamping = 0f;
                _cinemachineBrain.m_DefaultBlend.m_Time = 0.1f;
            }
        }
    }
}
