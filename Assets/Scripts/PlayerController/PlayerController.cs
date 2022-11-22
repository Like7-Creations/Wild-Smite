using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    PlayerInput lmao;
    [Header("Player Movement Settings")]
    CharacterController controller;
    public float playerSpeed;
    public float jumpHeight = 1;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector2 movementInput;
    [SerializeField] private float gravity = -9.81f;
    public GameObject playerPrefab;
    Animator animator;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        //playerStats = FindObjectOfType<CharacterStats>();
        /*var p1 = PlayerInput.Instantiate(playerPrefab,
            controlScheme: "Keyboard", device: Keyboard.current); //Split keyboard stuff
        var p2 = PlayerInput.Instantiate(playerPrefab,
            controlScheme: "Keyboard(2)", device: Keyboard.current);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");*/

        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
        controller.Move(new Vector3(0, -9.81f, 0));
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);


            //InvokeRepeating("lostStamina", 1f, 1);

        }

        if (direction != Vector3.zero)
        {
            animator.SetBool("Running", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dashing"))
        {
            animator.SetBool("Dashing", false);
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log("input detected");
    }
    public void Dash()
    {
        animator.SetBool("Dashing", true);
    }

   /* public virtual void loseStamina()
    {
        currStamina -= 1;
    }*/
}
