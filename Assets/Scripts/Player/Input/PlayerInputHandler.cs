using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int NormalizedDashDirectionInput { get; private set; }
    //public int NormalizedInputX { get; private set; }
    //public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool CrouchInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
    }

    private void Update()
    {
        CheckDashInputHoldTime();
        CheckJumpInputHoldTime();
    }


    #region Input Grab Functions
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        Debug.Log(RawMovementInput);
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }


    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }


    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        //// If we're using a keyboard/mouse/controller
        //if (playerInput.currentControlScheme == "Keyboard") // If we're using a mouse for the dash direction
        //{
        //    // This grabs the point our mouse is clicking on the screen, and subtracts our current player position to determine the dash direction
        //    RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        //}

        //NormalizedDashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }


    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchInput = true;
        }
        else if (context.canceled)
        {
            CrouchInput = false; 
        }
    }


    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        else if (context.canceled)
        {
            GrabInput = false;
        }
    }
    #endregion

    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;


    private void CheckJumpInputHoldTime()
    { // This holds a jump input we enter for 0.2 seconds while in the air, and if we touch the ground before the time is up it will jump again for us, this is awesome!
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime) ;
    }
}
