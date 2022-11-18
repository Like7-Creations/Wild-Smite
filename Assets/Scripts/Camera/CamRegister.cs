using UnityEngine;
using Cinemachine;

public class CamRegister : MonoBehaviour
{
    private void OnEnable()
    {
        CameraSwitcher.RegisterCam(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        CameraSwitcher.DeregisterCam(GetComponent<CinemachineVirtualCamera>());
    }
}
