using UnityEngine;

public class Player : MonoBehaviour
{
    # region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components    
    public PlayerInputHandler InputHandler { get; private set; } // This is like chaining values together via funnel. Playerinput get goes to the player, which then goes to the states. So each state as access to the inputs
    public Animator Anim { get; private set; } // We add the getters and setters so our states have access to the animator
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 workspace; // Everytime we want to apply velocity we don't have to create a new vector2 when we say what we want the velocity to be, just use this variable
    #endregion


    #region Unity Callback Functions 
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        FacingDirection = 1;

        StateMachine.Initialize(IdleState);
    }


    private void Update()
    {
        // Instead of running the logic for each and every mechanic, we run the LogicUpdate Function, and the state machine handle the individual mechanic within this call
        CurrentVelocity = rb.velocity; // Instead of calling rb.velocity.x/y multiple times, save memory and call it once
        StateMachine.CurrentState.LogicUpdate();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace; // Since we're changing the velocity to avoid the physics/logic update overwriting each other, set the current velocity to the new velocity
    }

    public void SetAirVelocityX(float velocity)
    {
        workspace.Set(velocity, 0);
        rb.AddForce(workspace, ForceMode2D.Force);
        workspace.Set(rb.velocity.x, rb.velocity.y); // Use the current velocity value instead maybe?
        workspace.x = Mathf.Clamp(workspace.x, -8.8f, 8.8f);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Check Functions
    public bool CheckIfTouchingGround()
    {
        int numberOfGroundCollisions = boxCollider.Cast(Vector2.down, new RaycastHit2D[10], playerData.groundCheckRadius, true);
        return numberOfGroundCollisions != 0 ? true : false;
        // The other way to do this with layermasks (Though cast is awesome, it creates a rectangle not a circle), is this:
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); // groundChekc is the transform of a gameobject attached to the player
    }

    public void CheckIfShouldFlip(float input)
    {
        if (input != 0 && input != FacingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0, 180.0f, 0);
    }
    #endregion
}
