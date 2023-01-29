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

    //Essential for Player Input
    PlayerConfig playerConfig;
    PlayerControls controls;

    [SerializeField]
    //MeshRenderer playerMesh;

    bool isPaused;
    GameObject PauseMenuObject;
    PauseMenuController pauseMenu;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();


        //Essential for Player Input
        controls = new PlayerControls();

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

    //Essential for Player Input
    public void InitialisePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        //playerMesh.material = pc.PlayerMat;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    //Essential for Player Input
    void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Movement.name)
        {
            OnMove(obj);
        }

        if (obj.action.name == controls.Player.PauseGame.name && obj.started)
        {
            Debug.Log($"Player {playerConfig.PlayerIndex + 1} Pressed Pause Key");
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
        pauseMenu.PauseGame(playerConfig);
    }
}
