using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public static class CamSwitcher
{
    static List<CinemachineVirtualCamera> p1_Cameras = new List<CinemachineVirtualCamera>();
    static List<CinemachineVirtualCamera> p2_Cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera active_P1Cam = null;
    public static CinemachineVirtualCamera active_P2Cam = null;

    public static bool IsActiveCamera(CinemachineVirtualCamera cam)
    {
        return cam == active_P1Cam;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;

        if (camera.CompareTag("Player1"))
        {
            active_P1Cam = camera;

            
            Debug.Log("Current Active Camera for Player 1: " + camera);
        }
        else if (camera.CompareTag("Player2"))
        {
            active_P2Cam = camera;
            Debug.Log("Current Active Camera for Player 2: " + camera);
        }

        foreach (CinemachineVirtualCamera c in p1_Cameras)
        {
            if(c != camera)
            {
                c.Priority = 0;
            }
        }

        foreach (CinemachineVirtualCamera c in p2_Cameras)
        {
            if (c != camera)
            {
                c.Priority = 0;
            }
        }

    }

    public static void RegisterCam(CinemachineVirtualCamera cam)
    {
        if (cam.CompareTag("Player1"))
        {
            p1_Cameras.Add(cam);
        }
        else if (cam.CompareTag("Player2"))
        {
            p2_Cameras.Add(cam);
        }
    }

    public static void UnregisterCam(CinemachineVirtualCamera cam)
    {
        if (cam.CompareTag("Player1"))
        {
            p1_Cameras.Remove(cam);
        }
        else if (cam.CompareTag("Player2"))
        {
            p2_Cameras.Remove(cam);
        }
    }
}
