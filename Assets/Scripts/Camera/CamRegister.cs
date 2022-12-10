using UnityEngine;
using Cinemachine;

public class CamRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CinemachineVirtualCamera vCam = GetComponent<CinemachineVirtualCamera>();
        CameraSwitcher.RegisterCam(GetComponent<CinemachineVirtualCamera>());

        CinemachineTargetGroup camTarget = FindObjectOfType<CinemachineTargetGroup>();

        vCam.m_LookAt = camTarget.transform;
    }

    private void OnDisable()
    {
        CameraSwitcher.DeregisterCam(GetComponent<CinemachineVirtualCamera>());
    }
}
