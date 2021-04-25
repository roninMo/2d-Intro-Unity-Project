using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    //public int NormalizedInputX { get; private set; }
    //public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;


    private void Update()
    {
        //CheckJumpInputHoldTime();
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        //Debug.Log(RawMovementInput);
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time;
        }
    }


    public void UseJumpInput() => JumpInput = false;


    private void CheckJumpInputHoldTime()
    { // This holds a jump input we enter for 0.2 seconds while in the air, and if we touch the ground before the time is up it will jump again for us, this is awesome!
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
