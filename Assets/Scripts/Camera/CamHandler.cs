using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CamHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera combatCam;

    private void OnEnable()
    {
        CamSwitcher.RegisterCam(followCam);
        CamSwitcher.RegisterCam(combatCam);

        CamSwitcher.SwitchCamera(followCam);
    }

    private void OnDisable()
    {
        CamSwitcher.UnregisterCam(followCam);
        CamSwitcher.UnregisterCam(combatCam);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Switching Cameras");
            if(CamSwitcher.IsActiveCamera(followCam))
            {
                Debug.Log("Entering Combat. Switching to Combat_Cam");
                CamSwitcher.SwitchCamera(combatCam);
            }
            else if(CamSwitcher.IsActiveCamera(combatCam))
            {
                Debug.Log("Exiting Combat. Switching to Follow_Cam");
                CamSwitcher.SwitchCamera(followCam);
            }
        }
    }
}
