using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float playerSpeed;
    float turnSmoothTime = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    private Vector2 movementInput;
    float turnSmoothVelocity;
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Vector3 refer;
    
    Animator animator;
    PlayerActions PA;

   /* public EnemyStatRange ESR;
    public EnemyStats ES;*/
    
    void Start()
    {
        PA = GetComponent<PlayerActions>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");*/

        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;
        refer = direction;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
        controller.Move(new Vector3(0, -9.81f, 0));
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            PA.Dashdir = moveDir;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("X", 1f, 0.05f, Time.deltaTime);
            }else animator.SetFloat("X", 0.5f, 0.05f, Time.deltaTime);
            //InvokeRepeating("lostStamina", 1f, 1);

        }
        else animator.SetFloat("X", 0f, 0.05f, Time.deltaTime);
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
