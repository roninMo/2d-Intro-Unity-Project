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
        if (context.started) // context.started is set to true when pressed, and then immediately set to false (input.GetButtonDown)
        {
            Debug.Log("Started jump!");
        }
        if (context.performed) // context.performed is set to true when pressed, and 
        {
            Debug.Log("Holding down jump!");
        }
        if (context.canceled)
        {
            Debug.Log("Released jump button");
        }
    }
}
