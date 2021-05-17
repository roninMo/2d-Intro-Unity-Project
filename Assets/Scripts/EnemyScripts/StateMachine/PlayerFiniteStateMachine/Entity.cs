using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; }
    public AnimationToStateMachineLink atsm { get; private set; }

    public int facingDirection { get; private set; }
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;

    private float currentHealth;
    private int lastDamageDirection;

    private Vector2 velocityWorkspace;


    public virtual void Start()
    {
        currentHealth = entityData.maxHealth;
        facingDirection = 1;

        aliveGameObject = transform.Find("Alive").gameObject;
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();
        atsm = aliveGameObject.GetComponent<AnimationToStateMachineLink>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    #region Functions

    #region Velocity Functions
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    #endregion


    #region Check Functions
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }


    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }


    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }


    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }


    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    #endregion


    #region Attack Functions
    public virtual void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;
        DamageHop(entityData.damageKnockbackForce);

        if (attackDetails.position.x > aliveGameObject.transform.position.x) // Where is the attack coming from?
        { // meaning it came from the right
            lastDamageDirection = -1;
        }
        else
        { // otherwise from the left
            lastDamageDirection = 1;
        }
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }
    #endregion


    #region Misc Functions
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0, 180.0f, 0);
    }
    #endregion
    #endregion

    #region Utilities
    public virtual void OnDrawGizmos()
    {
        // wallCheck gizmo example
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        // ledgeCheck gizmo example
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        // close range action distance
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)((Vector2.right * facingDirection) * entityData.closeRangeActionDistance), 0.3f);
        // min agro distance
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)((Vector2.right * facingDirection) * entityData.minAgroDistance), 0.2f);
        // max agro distance
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)((Vector2.right * facingDirection) * entityData.maxAgroDistance), 0.2f);
    }
    #endregion
}
