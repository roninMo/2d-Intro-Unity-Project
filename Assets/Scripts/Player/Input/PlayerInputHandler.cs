using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        Debug.Log(MovementInput);
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
    }
}
