using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    CharacterController controller;
    DisplayStats stats;
    public float playerSpeed;
    [HideInInspector] public float originalSpeed;
    public float jumpHeight = 1;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector2 movementInput;
    [SerializeField] private float gravity = -9.81f;
    public GameObject playerPrefab;
    Animator animator;
    public Vector3 refer;
    public float DashSpeed;
    public float DashTime;
    public TrailRenderer DashTrail;
    Vector3 Dashdir;

    public XPManager xp;
    
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stats = GetComponent<DisplayStats>();
        xp = GetComponent<XPManager>();
        originalSpeed = playerSpeed;
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
        refer = direction;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
        controller.Move(new Vector3(0, -9.81f, 0));
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            Dashdir = moveDir;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("X", 1f, 0.05f, Time.deltaTime);
            }else animator.SetFloat("X", 0.5f, 0.05f, Time.deltaTime);
            //InvokeRepeating("lostStamina", 1f, 1);

        }
        else animator.SetFloat("X", 0f, 0.05f, Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            DashTrail.emitting= true;
            StartCoroutine(Dashing());
        }

        /*if (direction != Vector3.zero)
        {
            animator.SetBool("Running", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("Idle", true);
        }*/


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dashing"))
        {
            animator.SetBool("Dashing", false);
            GetComponent<DisplayStats>().stat.dashStamina -= 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            xp.AddXp(10);
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetFloat("X", 1f, 1f, Time.deltaTime );
        }
        else animator.SetFloat("X", 0.5f, 0.1f, Time.deltaTime);*/
        // Debug.Log("input detected");
    }

    public IEnumerator Dashing()
    {
        float startTime = Time.time;

        while(Time.time < startTime + DashTime)
        {
            controller.Move(Dashdir * DashSpeed * Time.deltaTime);
            yield return null;
        }
        DashTrail.emitting = false;
        Debug.Log("Dashhinggg");
    }

    public void CheckForEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stats.stat.PlayerRangedAtk);
        foreach (Collider c in colliders)
        {
            if (c.GetComponent<DummyEnemy>())
            {
                c.GetComponent<DummyEnemy>().TakeDamage();
            }
        }
    }
}
