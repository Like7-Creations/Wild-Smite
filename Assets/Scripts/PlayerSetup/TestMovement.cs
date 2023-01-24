using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TestMovement : MonoBehaviour
{

    CharacterController controller;
    public float speed;
    
    Vector3 moveDir = Vector3.zero;
    Vector2 inputVector = Vector2.zero;

    PlayerConfig playerConfig;
    [SerializeField]
    MeshRenderer playerMesh;
    SetupInput controls;

    bool isPaused;
    GameObject PauseMenuObject;
    PauseMenuController pauseMenu;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        controls = new SetupInput();

        isPaused = false;
        PauseMenuObject = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu = PauseMenuObject.GetComponent<PauseMenuController>();
    }

    private void Update()
    {
        moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        moveDir = transform.TransformDirection(moveDir);
        moveDir *= speed;

        controller.Move(moveDir);
    }

    public void InitialisePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.PlayerMat;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.PlayerMovement.Movement.name)
        {
            OnMove(obj);
        }

        if (obj.action.name == controls.PlayerMovement.Pause.name)
        {            
            if (obj.performed)
                OnPause();
        }
    }

    void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    public void OnMove(CallbackContext context)
    {
        SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnPause()
    {
        if (isPaused)
        {
            isPaused = false;
            
        }
        else
        {
            isPaused = true;
            pauseMenu.PauseGame(playerConfig);
        }
    }
}
