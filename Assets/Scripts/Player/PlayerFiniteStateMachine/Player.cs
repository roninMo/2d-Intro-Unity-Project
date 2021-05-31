using UnityEngine;

public class Player : MonoBehaviour
{
    # region State Variables
    public Core Core { get; private set; }
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
    public PlayerDashState DashState { get; private set; }
    public PlayerCrouchIdleState CrouchIdleState { get; private set; }
    public PlayerCrouchMoveState CrouchMoveState { get; private set; }

    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components    
    public PlayerInputHandler InputHandler { get; private set; } // This is like chaining values together via funnel. Playerinput get goes to the player, which then goes to the states. So each state as access to the inputs
    public Animator Anim { get; private set; } // We add the getters and setters so our states have access to the animator
    public Rigidbody2D Rb { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public Transform DashDirectionIndicator { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    #endregion

    #region Other Variables
    private Vector2 workspace; // Everytime we want to apply velocity we don't have to create a new vector2 when we say what we want the velocity to be, just use this variable
    #endregion


    #region Unity Callback Functions 
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
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
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        DashDirectionIndicator = transform.Find("DashDirectionIndicator");
        Inventory = GetComponent<PlayerInventory>();

        // Initialize all the weapon states
        PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.primary]);
        //secondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.secondary]);

        StateMachine.Initialize(IdleState);
    }


    private void Update()
    {
        Core.LogicUpdate();

        // Instead of running the logic for each and every mechanic, we run the LogicUpdate Function, and the state machine handle the individual mechanic within this call
        StateMachine.CurrentState.LogicUpdate();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion


    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    public void SetColliderHeight(float height)
    {
        Vector2 center = BoxCollider.offset;
        workspace.Set(BoxCollider.size.x, height);

        center.y += (height - BoxCollider.size.y) / 2;

        BoxCollider.size = workspace;
        BoxCollider.offset = center;
    }
    #endregion
}
