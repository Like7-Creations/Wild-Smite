using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.LowLevel;

public class VirtualPointer : MonoBehaviour
{
    public PlayerInput playerInput;
    PlayerControls controls;
    Mouse virtualMouse;
    public RectTransform cursorTransform;
    public RectTransform canvasTransform;

    public float cursorSpeed = 800;
    public float cursorPadding = 35;
    bool previousCursorState;

    Vector2 cursorRawPos;
    bool clicked;


    private void OnEnable()
    {
        Cursor.visible = false;


        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.position;
            InputState.Change(virtualMouse.position, position);
        }

        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);
        InputSystem.onAfterUpdate += UpdateMotion;
        controls = new PlayerControls();

        if (playerInput.currentActionMap.Contains(controls.UI.MouseWarp) == null)
        {
            playerInput.SwitchCurrentActionMap("UI");

        }

        playerInput.onActionTriggered += DetectInput;
    }

    private void OnDisable()
    {
        InputSystem.RemoveDevice(virtualMouse);
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    void DetectInput(InputAction.CallbackContext context)
    {

        //if (context.action.name == controls.UI.MouseWarp.name)
        //    cursorRawPos = context.action.ReadValue<Vector2>();

        //if (context.action.name == controls.UI.CursorClick.name)
        //    clicked = context.action.IsPressed();
    }

    private void UpdateMotion()
    {
        if (virtualMouse == null)
        {
            return;
        }
        //if ()
        Vector2 pointerValue = cursorRawPos;
        pointerValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPos = virtualMouse.position.ReadValue();
        Vector2 newPos = currentPos + pointerValue;

        newPos.x = Mathf.Clamp(newPos.x, cursorPadding, Screen.width - cursorPadding);
        newPos.y = Mathf.Clamp(newPos.y, cursorPadding, Screen.height - cursorPadding);

        InputState.Change(virtualMouse.position, newPos);
        InputState.Change(virtualMouse.delta, pointerValue);

        if (previousCursorState != clicked)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, clicked);
            InputState.Change(virtualMouse, mouseState);
            previousCursorState = clicked;
        }

        AnchorCursor(newPos);
    }

    void AnchorCursor(Vector2 pos)
    {
        Vector2 anchorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, pos, null, out anchorPosition);
        cursorTransform.anchoredPosition = anchorPosition;
    }
}
