using UnityEngine;

public class Player : MonoBehaviour
{
    # region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components    
    public PlayerInputHandler InputHandler { get; private set; } // This is like chaining values together via funnel. Playerinput get goes to the player, which then goes to the states. So each state as access to the inputs
    public Animator Anim { get; private set; } // We add the getters and setters so our states have access to the animator
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region Other Variables
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    private Vector2 workspace; // Everytime we want to apply velocity we don't have to create a new vector2 when we say what we want the velocity to be, just use this variable
    #endregion


    #region Unity Callback Functions 
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();

        facingDirection = 1;

        StateMachine.Initialize(IdleState);
    }


    private void Update()
    {
        // Instead of running the logic for each and every mechanic, we run the LogicUpdate Function, and the state machine handle the individual mechanic within this call
        currentVelocity = rb.velocity; // Instead of calling rb.velocity.x/y multiple times, save memory and call it once
        StateMachine.CurrentState.LogicUpdate();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region My Functions
    #region Set Functions
    public void setVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        rb.velocity = workspace;
        currentVelocity = workspace; // Since we're changing the velocity to avoid the physics/logic update overwriting each other, set the current velocity to the new velocity
    }
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(float input)
    {
        if (input != 0 && input != facingDirection)
        {
            Flip();
        }
    }
    #endregion

    #region Other Functions
    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180.0f, 0);
    }
    #endregion
    #endregion
}
