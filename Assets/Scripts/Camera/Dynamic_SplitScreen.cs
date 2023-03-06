using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_SplitScreen : MonoBehaviour
{
    //Two Cameras that are initialized aand setup in the Start function
    public GameObject p1_Cam;
    public GameObject p2_Cam;

    public GameObject camTracker_P1;
    public GameObject camTracker_P2;

    public Vector3 camTrack1;
    public Vector3 camTrack2;


    //Two quads used to draw the 2nd screen and are setup in the start function.
    private GameObject splitterObj;    //CammChild
    private GameObject splitObj;       //GrandCamChild


    //Player Stuff
    public Transform player1;
    public Transform player2;

    int playerCount;

    //Minimum Distance Required To Split The Screen
    [Range(0f, 10f)]
    public float splitDistance;


    //Width and Color of the Splitter
    [Range(0f, 10f)]
    public float splitterWidth;
    public Color splitterColor;


    //Material Assigned to camChild's Render
    public Material unlitMat;
    public Material splitMat;

    public Shader SplitScreen;

    void Start()
    {
        Camera c1 = p1_Cam.GetComponent<Camera>();
        Camera c2 = p2_Cam.GetComponent<Camera>();

        //Logic for Coop or solo
        playerCount = PlayerConfigManager.Instance.GetPlayerConfigs().Count;

        //Set Cam2 to be rendered first in the render order.
        c2.depth = c1.depth - 1;
        //Set Cam2 to ignore the TransparentFX layer, so that the splitter is only rendered forCam1
        c2.cullingMask = ~(1 << LayerMask.NameToLayer("TransparentFX"));
        //c2.cullingMask = ~(2 << LayerMask.NameToLayer("Player1 Cam"));

        p2_Cam.SetActive(false);
        camTracker_P2.SetActive(false);

        //Begin initializing the Splitter game Obj [camChild]. 
        splitterObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        splitterObj.name = "Splitter";
        splitterObj.transform.parent = gameObject.transform;
        splitterObj.transform.localPosition = Vector3.forward;
        splitterObj.transform.localScale = new Vector3(2, splitterWidth / 10, 1);
        splitterObj.transform.localEulerAngles = Vector3.zero;
        splitterObj.SetActive(false);

        //Begin initializing the Split game Obj [grandCamChild].
        splitObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        splitObj.name = "Split";
        splitObj.transform.parent = splitterObj.transform;
        splitObj.transform.localPosition = new Vector3(0, -(1 / (splitterWidth / 10)), 0.0001f);
        splitObj.transform.localScale = new Vector3(1, 2 / (splitterWidth / 10), 1);
        splitObj.transform.localEulerAngles = Vector3.zero;


        //Begin creating the temporary materials required for the splitscreen
        Material tempUnlit = unlitMat;
        tempUnlit.color = splitterColor;
        splitterObj.GetComponent<Renderer>().material = tempUnlit;
        splitterObj.GetComponent<Renderer>().sortingOrder = 2;
        splitterObj.layer = 1;      //LayerMask.NameToLayer("TransparentFx");

        Material tempSplitScreen = splitMat;

        splitObj.GetComponent<Renderer>().material.shader = SplitScreen;

        splitObj.GetComponent<Renderer>().material = tempSplitScreen;
        splitObj.layer = 1;         //LayerMask.NameToLayer("TransparentFx")
    }

    public void AddPlayer(GameObject player, int playerIndex)
    {
        if (playerCount == 1)
        {
            camTracker_P1.GetComponent<CamTrackerMove>().target = player;
            player1 = player.transform;
        }
        else if (playerCount == 2)
        {
            if (playerIndex == 0)
            {
                camTracker_P1.GetComponent<CamTrackerMove>().target = player;
                player1 = player.transform;
            }
            else if (playerIndex == 1)
            {

                camTracker_P2.GetComponent<CamTrackerMove>().target = player;
                player2 = player.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (playerCount == 2)
        {
            //Find distance between both players along z-axis.
            float zDist = player1.position.z - player2.position.z;
            float dist = Vector3.Distance(player1.position, player2.position);


            //Determine angle of which player's cam should be projected on the upper quadrants of the screen, based on their x.Pos value
            float splitterAngle;

            if (player1.position.x <= player2.position.x)
            {
                splitterAngle = Mathf.Rad2Deg * Mathf.Acos(zDist / dist);
            }
            else
            {
                splitterAngle = Mathf.Rad2Deg * Mathf.Asin(zDist / dist) - 90;
            }

            //The Splitter is then rotated along the z-axis based on the resulting value.
            splitterObj.transform.localEulerAngles = new Vector3(0, 0, splitterAngle);


            //Determine precise midpoint between players and store it in a temp variable called 'midpoint'.
            Vector3 midpoint = new Vector3((player1.position.x + player2.position.x) / 2, (player1.position.y + player2.position.y) / 2, (player1.position.z + player2.position.z) / 2);

            //Check if "distance" between two players is greater than the splitDistance
            if (dist > splitDistance)
            {
                //Code for camera tracking. Delete later.

                Vector3 p1_Offset = midpoint - player1.position;

                p1_Offset.x = Mathf.Clamp(p1_Offset.x, -splitDistance / 2, splitDistance / 2);
                p1_Offset.y = Mathf.Clamp(p1_Offset.y, -splitDistance / 2, splitDistance / 2);
                p1_Offset.z = Mathf.Clamp(p1_Offset.z, -splitDistance / 2, splitDistance / 2);

                midpoint = player1.position + p1_Offset;
                //Debug.Log(p1_Offset);



                Vector3 p2_Offset = midpoint - player2.position;

                p2_Offset.x = Mathf.Clamp(p2_Offset.x, -splitDistance / 2, splitDistance / 2);
                p2_Offset.y = Mathf.Clamp(p2_Offset.y, -splitDistance / 2, splitDistance / 2);
                p2_Offset.z = Mathf.Clamp(p2_Offset.z, -splitDistance / 2, splitDistance / 2);

                Vector3 midpoint2 = player2.position - p2_Offset;
                //Debug.Log(p2_Offset);

                //Update CamTracker Offsets to ensure the players remain visible.
                camTrack1 = p1_Offset + Vector3.one;
                camTrack2 = p2_Offset + Vector3.one;


                if (!splitterObj.activeSelf)
                {
                    splitterObj.SetActive(true);
                    p2_Cam.SetActive(true);
                    camTracker_P2.SetActive(true);

                    p2_Cam.transform.position = p1_Cam.transform.position;
                    p2_Cam.transform.rotation = p1_Cam.transform.rotation;
                }
                else
                {
                    //Failed Attempts-----

                    /*Calculate the split offset, by adding the midpoint2 with the vCam's body offset
                    Vector3 p2_vOffset = p2_Vcam.GetComponentInChildren<CinemachineTransposer>().m_FollowOffset;

                    p2_Vcam.GetComponentInParent<CamTrackerMove>().splitOffset = 
                        new Vector3(p2_Offset.x, 0, p2_Offset.z) + new Vector3(p2_vOffset.x, 0, p2_vOffset.z);*/

                    /*Quaternion p2_CamRot = Quaternion.LookRotation(midpoint2 - p2_Cam.transform.position);
                    p2_Cam.transform.rotation = Quaternion.Lerp(p2_Cam.transform.rotation, p2_CamRot, Time.deltaTime * 5);*/

                    /*//-----
                    CinemachineTransposer cam1_Transposer = p1_Vcam.GetComponentInChildren<CinemachineTransposer>();

                    Vector3 p1_CamRot = midpoint - p1_Cam.transform.position;

                    cam1_Transposer.m_FollowOffset = Vector3.Lerp(p1_Cam.transform.position, p1_CamRot, Time.deltaTime * 5);
                    //-----

                    //-----
                    CinemachineTransposer cam2_Transposer = p2_Vcam.GetComponentInChildren<CinemachineTransposer>();

                    Vector3 p2_CamRot = midpoint2 - p2_Cam.transform.position;

                    cam2_Transposer.m_FollowOffset = Vector3.Lerp(p2_Cam.transform.position, p2_CamRot, Time.deltaTime * 5);
                    //-----*/
                    //Failed Attempts-----
                }


            }
            else
            {
                //Reset CamTracker Offsets to default
                camTrack1 = Vector3.zero;
                camTrack2 = Vector3.zero;

                if (splitterObj.activeSelf)
                {
                    splitterObj.SetActive(false);
                    p2_Cam.SetActive(false);
                    camTracker_P2.SetActive(false);
                }
            }

            //Figure out how to track player movement when the screen is split.
            //Need to somehow input some data into the CamTracker so that it will change its position to ensure the player is in view.


            //Failed Attempts-----
            /*Vector3 p1_vOffset = p1_Vcam.GetComponentInChildren<CinemachineTransposer>().m_FollowOffset;

            p1_Vcam.GetComponentInParent<CamTrackerMove>().splitOffset =
                new Vector3(p1_Offset.x, 0, p1_Offset.z) + new Vector3(p1_vOffset.x, 0, p1_vOffset.z);
            Debug.Log(midpoint);*/

            /*Quaternion p1_CamRot = Quaternion.LookRotation(midpoint - p1_Cam.transform.position);
            p1_Cam.transform.rotation = Quaternion.Lerp(p1_Cam.transform.rotation, p1_CamRot, Time.deltaTime * 5);*/

            /*CinemachineTransposer cam1_Transposer = p1_Vcam.GetComponentInChildren<CinemachineTransposer>();

            Vector3 p1_CamRot = midpoint - p1_Cam.transform.position;

            cam1_Transposer.m_FollowOffset = Vector3.Lerp(p1_Cam.transform.position, p1_CamRot, Time.deltaTime * 5);*/
            //Failed Attempts-----
        }
    }
}
