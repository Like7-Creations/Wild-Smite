using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CamHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera combatCam;

    [SerializeField] int activePriorityVal;
    [SerializeField] int inActivePriorityVal;

    CinemachineVirtualCamera currentCam;

    void Start()
    {
        if (!followCam.gameObject.activeSelf)
        {
            followCam.gameObject.SetActive(true);
        }
        currentCam = followCam;

        combatCam.gameObject.SetActive(false);
        combatCam.Priority++;
    }

    /*private void OnEnable()
    {
        CamSwitcher.RegisterCam(followCam);
        CamSwitcher.RegisterCam(combatCam);

        CamSwitcher.SwitchCamera(followCam);
    }

    private void OnDisable()
    {
        CamSwitcher.UnregisterCam(followCam);
        CamSwitcher.UnregisterCam(combatCam);
    }*/

    /*void Update()
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
    }*/

    public void SwitchVirtualCamera()
    {
        //Check which camera is the current camera
        if (currentCam == followCam)
        {
            combatCam.gameObject.SetActive(true);
            combatCam.Priority++;
            currentCam = combatCam;

            followCam.Priority--;
            followCam.gameObject.SetActive(false);
        }
        else if (currentCam == combatCam)
        {
            followCam.gameObject.SetActive(true);
            followCam.Priority++;
            currentCam = followCam;

            currentCam.Priority--;
            currentCam.gameObject.SetActive(false);
        }
        else if (currentCam == null)
        {
            currentCam = followCam;
        }
        //Switch current camera to the inactive one.
        //Decrement the newly inactive camera's priority
        //Disable the newly inactive camera.
    }

    void SwitchToFollow()
    {

    }

    void SwitchToCombat()
    {

    }
}
