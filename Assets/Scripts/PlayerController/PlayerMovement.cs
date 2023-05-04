using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float playerSpeed;
    float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    private Vector2 movementInput;
    public float turnSmoothVelocity;
    Vector3 velocity;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Vector3 refer;
    public Vector3 dot;

    [HideInInspector] public Animator animator;
    PlayerActions PA;
    PlayerControl Pc;
    PlayerControls controls;

    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;
    float turnAmount;
    float forwardAmount;

    public SkyBoxGen weather;
    GameObject[] rains;

    // Just testing stuff 
    public Camera cam;
    public float xOffset;
    public float yOffset;
    public float zOffset;

    void Start()
    {
        // for testing
        weather.ManageWeather();
        cam = Camera.main;
        Pc = GetComponent<PlayerControl>();
        controls = Pc.GetControls();
        PA = GetComponent<PlayerActions>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //Pc.OnInputAction += Input_onActionTriggered;
        //xOffset = 5.7f;
        //yOffset = 13.31f;
        //zOffset = -8.2f;
        xOffset = 0;
        yOffset = 3;
        zOffset = -3f;
    }

    void Update()
    {


        Vector3 pos = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);

        cam.transform.position = Vector3.Lerp(cam.transform.position, pos, 4 * Time.deltaTime);
        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y).normalized; //should be movementinput.x,0,movementinput.y
        refer = direction;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);

        //Farhan's Code-----
        AudioSource walkSource = GetComponent<Player_SFXHandler>().moveLoopAudio;
        //Farhan's Code-----

        if (direction != Vector3.zero)
        {
            if (!PA.shooting)
            {
                transform.rotation = Quaternion.Euler(0, angle, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            PA.Dashdir = moveDir; // this is for dash to Make the player dash to their forward

            Vector3 backDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.back;
            PA.knockBackDir = backDir; // This is  for the knockback when the player gets hit.

            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
            animator.SetFloat("X", 0.5f, 0.05f, Time.deltaTime);

            //VFX Walk
            PA.trigger_walkVFX.Invoke();
            //VFX Walk

            //Farhan's Code-----
            //if (!walkSource.isPlaying)
            //{
                //PA.trigger_walkSFX.Invoke();
           //}
            //Farhan's Code-----

            Vector3 lookDirection = transform.forward;
            dot = controller.velocity;




            if (PA.isSprinting)
            {
                //animator.SetBool("Sprinting", PA.isSprinting);
                animator.SetFloat("Sprinting", 2);
                //InvokeRepeating("lostStamina", 1f, 1);
            }
            else animator.SetFloat("Sprinting", 1);


        }
        else
        {
            animator.SetFloat("X", 0f, 0.05f, Time.deltaTime);
            //animator.SetBool("Sprinting", false);

            //Farhan's Code-----
            //if (walkSource.isPlaying)
           // {
                //PA.trigger_walkSFX.Invoke();
            //}
            //Farhan's Code-----
        }

        float forwardDirection = Vector3.Dot(transform.forward, new Vector3(direction.x, 0, direction.z));
        float rightDirection = Vector3.Dot(transform.right, new Vector3(direction.x, 0f, direction.z));

        forwardDirection = Mathf.RoundToInt(forwardDirection);
        rightDirection = Mathf.RoundToInt(rightDirection);


        animator.SetFloat("X", rightDirection);
        animator.SetFloat("Y", forwardDirection);
        if (PA.shooting)
        {
        }

        if(!controller.isGrounded)
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        animCheck();
    }

    public void animCheck()
    {
        animator.SetBool("Moving", true);
    }

    public void knockUp()
    {
        velocity.y = Mathf.Sqrt(1 * -2f * gravity);
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    /*void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Movement.name)
        {
            onMove(obj);
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + dot.normalized);
    }
}
