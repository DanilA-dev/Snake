using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private CinemachineVirtualCamera cinemachineCamera;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float time, float intensity)
    {
        var cameraShakeNoise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StartCoroutine(ShakeTic(time, intensity, cameraShakeNoise));

    }

    private IEnumerator ShakeTic(float intensity ,float time, CinemachineBasicMultiChannelPerlin cam)
    {
        cam.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(time);
        cam.m_AmplitudeGain = 0f;
    }
}
