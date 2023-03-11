using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    PlayerConfig playerConfig;
    PlayerControls controls;
    PlayerActions pActions;
    PlayerMovement pMovement;

    public Renderer[] matMeshes;

    private void Awake()
    {
        pMovement = GetComponent<PlayerMovement>();
        pActions = GetComponent<PlayerActions>();

    }

    void Update()
    {
        pActions.Rotation();
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        //Dashing
        if (context.action.name == controls.Player.Dash.name && context.performed)
        {
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
            pActions.Attack();
        }

        // GamePad Rotation
        /* if (context.action.name == controls.Player.Rotation.name && context.performed)
         {
             //pActions.shooting = true;
         }

         if (context.action.name == controls.Player.Rotation.name && context.canceled)
         {
             //pActions.shooting = false;
         }*/

        //GamePad Range Attack
        if (context.action.name == controls.Player.GamePadRangeAttack.name && context.performed)
        {
            pActions.shooting = true;
        }

        if (context.action.name == controls.Player.GamePadRangeAttack.name && context.canceled)
        {
            pActions.shooting = false;
        }

        //Keyboard and Mouse Range Attack
        if (context.action.name == controls.Player.RangeAttack.name && context.performed)
        {
            pActions.shooting = true;
            pActions.mouseShooting = true;
        }
        if (context.action.name == controls.Player.RangeAttack.name && context.canceled)
        {
            pActions.shooting = false;
            pActions.mouseShooting = false;
        }

        // AOE
        if (context.action.name == controls.Player.AreaOfEffect.name && context.performed)
        {
            pActions.charging = true;
        }

        if (context.action.name == controls.Player.AreaOfEffect.name && context.canceled)
        {
            pActions.AOE(pActions.pStats.aoe_Tap);
        }

        // Sprinting
        if (context.action.name == controls.Player.Sprinting.name && context.performed)
        {
            pActions.Sprint();
        }
        if (context.action.name == controls.Player.Sprinting.name && context.canceled)
        {
            pActions.UnSprint();
        }

        // Pause Game
        if (context.action.name == controls.Player.PauseGame.name && context.performed)
        {
            PauseMenuController pause = FindObjectOfType<PauseMenuController>();
            if (pause != null)
                pause.PauseGame(playerConfig);
        }
    }

    public void InitialisePlayer(PlayerConfig pc)
    {
        playerConfig = pc;
        controls = new PlayerControls();
        GetComponent<PlayerStats>().SetData(pc.playerStats);
        pActions = GetComponent<PlayerActions>();
        playerConfig.Input.onActionTriggered += OnInputAction;
        //playerMesh.material = pc.PlayerMat;
        if (matMeshes.Length > 0)
            for (int i = 0; i < matMeshes.Length; i++)
                matMeshes[i].material = playerConfig.PlayerMat;

        if (FindObjectOfType<Dynamic_SplitScreen>() != null)
            FindObjectOfType<Dynamic_SplitScreen>().AddPlayer(this.gameObject, playerConfig.PlayerIndex);
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
