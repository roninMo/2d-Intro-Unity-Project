using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Started jump!");
        }
        if (context.performed)
        {
            Debug.Log("Holding down jump!");
        }
        if (context.canceled)
        {
            Debug.Log("Released jump button");
        }
    }
}
