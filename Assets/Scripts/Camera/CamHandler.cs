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
            //Instead of switching to a completely different camera, talk to Chris about simply changing values.
            //We can have preset values in the script which will be fed into the VCam when switching.

            #region Attempt 1
            combatCam.gameObject.SetActive(true);
            combatCam.Priority++;
            currentCam = combatCam;

            followCam.Priority--;
            followCam.gameObject.SetActive(false);
            #endregion
        }
        else if (currentCam == combatCam)
        {
            //Instead of switching to a completely different camera, talk to Chris about simply changing values.
            //We can have preset values in the script which will be fed into the VCam when switching.

            #region Attempt 1
            followCam.gameObject.SetActive(true);
            followCam.Priority++;
            currentCam = followCam;

            currentCam.Priority--;
            currentCam.gameObject.SetActive(false);
            #endregion
        }
        else if (currentCam == null)
        {
            //Set some values as the default values that the camera can use when necessary.

            currentCam = followCam;     //Attempt 1
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
