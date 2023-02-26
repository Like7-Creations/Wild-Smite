using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    PlayerConfig playerConfig;
    PlayerControls controls;
    PlayerActions pActions;
    PlayerMovement pMovement;


    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        pActions = GetComponent<PlayerActions>();
    }

    void Update()
    {
        // pActions.aim = controls.Player.Rotation.ReadValue<Vector2>();
        pActions.Rotation();
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        //Dashing
        if (context.action.name == controls.Player.Dash.name && context.performed)
        {
            Debug.Log("Dashing Called");
            pActions.Dash();
            //StartCoroutine(pActions.Dashing());
        }

        //Player Movement
        if (context.action.name == controls.Player.Movement.name && context.performed)
        {
            //Debug.Log("Movement Called");
            //pMovement.animator.SetFloat("X", 0.5f, 0.05f, Time.deltaTime);
            pMovement.onMove(context);
        }

        //Melee Attack
        if (context.action.name == controls.Player.Attack.name && context.performed)
        {
            Debug.Log("Attack Called");
            pActions.Attack();
        }

        // GamePad RangeAttack
        if (context.action.name == controls.Player.Rotation.name && context.performed)
        {
            pActions.shooting = true;
        }

        if (context.action.name == controls.Player.Rotation.name && context.canceled)
        {
            pActions.shooting = false;
        }

        //Keyboard and Mouse Range Attack
        if (context.action.name == controls.Player.RangeAttack.name && context.performed)
        {
            pActions.shooting = true;
        }
        if (context.action.name == controls.Player.RangeAttack.name && context.canceled)
        {
            pActions.shooting = false;
        }
        
        // AOE
        if (context.action.name == controls.Player.AreaOfEffect.name && context.performed)
        {
            Debug.Log("AOE Called");
            pActions.AOE();
        }

        // Sprinting
        if (context.action.name == controls.Player.Sprinting.name && context.performed)
        {
            Debug.Log("sprinting Called");
           // pMovement.animator.SetFloat("X", 1f, 0.05f, Time.deltaTime);
            pActions.Sprint();
        }
        if (context.action.name == controls.Player.Sprinting.name && context.canceled)
        {
            Debug.Log("unsprint Called");
            //pMovement.animator.SetFloat("X", 0.5f, 0.05f, Time.deltaTime);
            pActions.UnSprint();
        }
    }

    public void InitialisePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        controls = new PlayerControls();
        pActions = GetComponent<PlayerActions>();
        playerConfig.Input.onActionTriggered += OnInputAction;
        //playerMesh.material = pc.PlayerMat;
    }

    public PlayerConfig GetConfig()
    {
        return playerConfig;
    }

    public PlayerControls GetControls() 
    {
        return controls;
    }
}
