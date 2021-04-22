using UnityEngine;

public class OldPlayerInput : MonoBehaviour
{
    // Character Inputs
    [HideInInspector] public float movementInput;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool crouch;
    [HideInInspector] public bool holdingJump;
    [HideInInspector] public bool holdingCrouch;
    [HideInInspector] public bool dash;
    [HideInInspector] public bool attack;
    [HideInInspector] public bool autoAttack;

    // Menu Inputs
    //[HideInInspector] public bool pause; // esc
    //[HideInInspector] public bool menu; // tab
    //[HideInInspector] public bool inventory; // i

    void Update()
    {
        // Grab the direction they're pressing
        movementInput = Input.GetAxis("Horizontal");

        // Jump inputs
        jump = Input.GetButtonDown("Jump") ? true : false;
        holdingJump = Input.GetButton("Jump") ? true : false;

        // Crouch inputs
        crouch = Input.GetButtonDown("Crouch") ? true : false;
        holdingCrouch = Input.GetButton("Crouch") ? true : false;

        // Dash inputs
        dash = Input.GetButtonDown("Dash") ? true : false;

        // attack inputs
        attack = Input.GetButtonDown("Attack") ? true : false;
        attack = Input.GetButton("Attack") ? true : false;
    }
}
