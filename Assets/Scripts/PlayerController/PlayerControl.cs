using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector] public PlayerConfig playerConfig;
    public bool controlScheme;
    PlayerControls controls;
    PlayerActions pActions;
    PlayerMovement pMovement;
    PlayerInventory pInventory;

    bool isPaused;

    public Renderer[] matMeshes;
    Animator anim;

    private void Awake()
    {
        pMovement = GetComponent<PlayerMovement>();
        pActions = GetComponent<PlayerActions>();
        pInventory = GetComponent<PlayerInventory>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        pActions.Rotation();
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        if (!playerConfig.isPaused)
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

         //GamePad Rotation
         if (context.action.name == controls.Player.Rotation.name && context.performed)
         {
             //pActions.shooting = true;
         }

         if (context.action.name == controls.Player.Rotation.name && context.canceled)
         {
             //pActions.shooting = false;
         }

            //GamePad Range Attack
            if (context.action.name == controls.Player.GamePadRangeAttack.name && context.performed)
            {
                pActions.shooting = true;
                //anim.SetLayerWeight(anim.GetLayerIndex("Shooting Layer"), 1);
            }

            if (context.action.name == controls.Player.GamePadRangeAttack.name && context.canceled)
            {
                pActions.shooting = false;
                // anim.SetLayerWeight(anim.GetLayerIndex("Shooting Layer"), 0);
            }

            //Keyboard and Mouse Range Attack
            if (context.action.name == controls.Player.RangeAttack.name && context.performed)
            {
                pActions.shooting = true;
                pActions.mouseShooting = true;

                UnityEngine.Debug.Log("Player is shooting");
                //anim.SetLayerWeight(anim.GetLayerIndex("Shooting Layer"), 1);
            }
            if (context.action.name == controls.Player.RangeAttack.name && context.canceled)
            {
                pActions.shooting = false;
                pActions.mouseShooting = false;
                //anim.SetLayerWeight(anim.GetLayerIndex("Shooting Layer"), 0);
            }

            // AOE
            if (context.action.name == controls.Player.AreaOfEffect.name && context.performed)
            {
                pActions.charging = true;
            }

            if (context.action.name == controls.Player.AreaOfEffect.name && context.canceled)
            {
                pActions.ReleaseAOE(pActions.chargedSTAM, pActions.chargedMELEE, pActions.chargedRANGE);
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

            // Use Item 
            if (context.action.name == controls.Player.UseItem.name && context.performed)
            {
                pInventory.useItem();
            }
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
        string scheme = playerConfig.Input.user.controlScheme.ToString();
        controlScheme = scheme == "Controller(<Gamepad>)" ? true : false;
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
