using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCam = null;

    public static bool IsActiveCam(CinemachineVirtualCamera cam)
    {
        return cam == ActiveCam;
    }

    public static void RegisterCam(CinemachineVirtualCamera newCam)
    {
        cameras.Add(newCam);
        Debug.Log(newCam + "has been registered.");
    }

    public static void DeregisterCam(CinemachineVirtualCamera oldCam)
    {
        cameras.Remove(oldCam);
        Debug.Log(oldCam + "has been deregistered.");
    }

    public static void SwitchCam(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        ActiveCam = cam;

        foreach(CinemachineVirtualCamera c in cameras)
        {
            if(c != cam && c.Priority != 0)
                c.Priority = 0;
        }
    }
}
