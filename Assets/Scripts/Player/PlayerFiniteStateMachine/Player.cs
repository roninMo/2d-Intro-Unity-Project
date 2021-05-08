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
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components    
    public PlayerInputHandler InputHandler { get; private set; } // This is like chaining values together via funnel. Playerinput get goes to the player, which then goes to the states. So each state as access to the inputs
    public Animator Anim { get; private set; } // We add the getters and setters so our states have access to the animator
    public Rigidbody2D rb { get; private set; }
    public BoxCollider2D boxCollider { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

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
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        dashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
        crouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        crouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");

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
    public void SetVelocityToZero()
    {
        rb.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }


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
        workspace.x = Mathf.Clamp(workspace.x, -9.4f, 9.4f);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }


    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
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


    public bool CheckIfTouchingCeiling()
    {
        int numberOfCeilingCollisions = boxCollider.Cast(Vector2.up, new RaycastHit2D[10], playerData.ceilingCheckDistance, true);
        return numberOfCeilingCollisions != 0 ? true : false;
        // The other way to do this with layermasks (Though cast is awesome, it creates a rectangle not a circle), is this:
        //return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround); // groundChekc is the transform of a gameobject attached to the player
    }


    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }


    public bool CheckIfBackTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }


    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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


    public void SetColliderHeight(float height)
    {
        Vector2 center = boxCollider.offset;
        workspace.Set(boxCollider.size.x, height);

        center.y += (height - boxCollider.size.y) / 2;

        boxCollider.size = workspace;
        boxCollider.offset = center;
    }


    public Vector2 DetermineCornerPosition()
    {
        // This function takes our wall and ledge check positions, and creates a raycast in the y direction between them to calculate where the ledge is exactly
        // So - The wallCheck finds the distance from the player to the side of the wall, then stores it
        // The raycast we create goes out that distance from from the ledgeCheck, and then goes down to find the distance to the top of the ledge and stores it
        // this is how it determines the corner position and where our player should climb up to

        RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right, playerData.wallCheckDistance, playerData.whatIsGround);
        float xDistance = xHit.distance;
        workspace.Set(xDistance * FacingDirection, 0f); // the distance the player is from the wall
        RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);
        float yDistance = yHit.distance;

        workspace.Set(wallCheck.position.x + (xDistance * FacingDirection), ledgeCheck.position.y - yDistance);
        return workspace;
    }
    #endregion
}
