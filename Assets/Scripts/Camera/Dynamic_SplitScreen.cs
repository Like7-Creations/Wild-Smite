using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_SplitScreen : MonoBehaviour
{
    //Two Cameras that are initialized aand setup in the Start function
    public GameObject p1_Cam;
    public CinemachineVirtualCamera p1_Vcam;
    
    public GameObject p2_Cam;
    public CinemachineVirtualCamera p2_Vcam;



    //Two quads used to draw the 2nd screen and are setup in the start function.
    private GameObject splitterObj;    //CammChild
    private GameObject splitObj;       //GrandCamChild


    //Player Transforms
    public Transform player1;
    public Transform player2;


    //Minimum Distance Required To Split The Screen
    [Range(0f, 10f)]
    public float splitDistance;


    //Width and Color of the Splitter
    [Range(0f, 10f)]
    public float splitterWidth;
    public Color splitterColor;


    //Material Assigned to camChild's Render
    public Material unlitMat;

    void Start()
    {
        Camera c1 = p1_Cam.GetComponent<Camera>();
        Camera c2 = p2_Cam.GetComponent<Camera>();


        //Set Cam2 to be rendered first in the render order.
        c2.depth = c1.depth - 1;
        //Set Cam2 to ignore the TransparentFX layer, so that the splitter is only rendered forCam1
        c2.cullingMask = ~(1 << LayerMask.NameToLayer("TransparentFX"));
        c2.cullingMask = ~(1 << LayerMask.NameToLayer("Player1 Cam"));

        p2_Cam.SetActive(false);

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

        Material tempSplitScreen = new Material(Shader.Find("Mask/SplitScreen"));

        splitObj.GetComponent<Renderer>().material = tempSplitScreen;
        splitObj.layer = 1;         //LayerMask.NameToLayer("TransparentFx")
    }

    void LateUpdate()
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



            Vector3 p2_Offset = midpoint - player1.position;

            p2_Offset.x = Mathf.Clamp(p2_Offset.x, -splitDistance / 2, splitDistance / 2);
            p2_Offset.y = Mathf.Clamp(p2_Offset.y, -splitDistance / 2, splitDistance / 2);
            p2_Offset.z = Mathf.Clamp(p2_Offset.z, -splitDistance / 2, splitDistance / 2);

            Vector3 midpoint2 = player2.position + p2_Offset;


            if (!splitterObj.activeSelf)
            {
                splitterObj.SetActive(true);
                p2_Cam.SetActive(true);

                p2_Cam.transform.position = p1_Cam.transform.position;
                p2_Cam.transform.rotation = p1_Cam.transform.rotation;
            }
            else
            {
                Quaternion p2_CamRot = Quaternion.LookRotation(midpoint2 - p2_Cam.transform.position);
                p2_Cam.transform.rotation = Quaternion.Lerp(p2_Cam.transform.rotation, p2_CamRot, Time.deltaTime * 5);

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
            }
        }
        else
        {
            if (splitterObj.activeSelf)
            {
                splitterObj.SetActive(false);
                p2_Cam.SetActive(false);
            }
        }

        Quaternion p1_CamRot = Quaternion.LookRotation(midpoint - p1_Cam.transform.position);
        p1_Cam.transform.rotation = Quaternion.Lerp(p1_Cam.transform.rotation, p1_CamRot, Time.deltaTime * 5);


        /*CinemachineTransposer cam1_Transposer = p1_Vcam.GetComponentInChildren<CinemachineTransposer>();

        Vector3 p1_CamRot = midpoint - p1_Cam.transform.position;

        cam1_Transposer.m_FollowOffset = Vector3.Lerp(p1_Cam.transform.position, p1_CamRot, Time.deltaTime * 5);*/
    }
}
