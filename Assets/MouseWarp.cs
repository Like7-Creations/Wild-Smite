using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWarp : MonoBehaviour
{

    PlayerInput input;
    PlayerControls controls;
    bool mouseWarpEnabled;

    // Start is called before the first frame update
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        if (input.currentControlScheme.ToString() == "Controller")
            mouseWarpEnabled = true;
        else
            mouseWarpEnabled = false;

        controls = new PlayerControls();
        input.onActionTriggered += UdateMousePos;
    }

    // Update is called once per frame
    void UdateMousePos(InputAction.CallbackContext context)
    {
        if (context.action.name == controls.UI.MouseWarp.name)
            if (mouseWarpEnabled)
                Mouse.current.WarpCursorPosition(context.ReadValue<Vector2>());
    }
}
