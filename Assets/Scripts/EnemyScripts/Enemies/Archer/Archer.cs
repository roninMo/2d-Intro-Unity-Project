using UnityEngine;

public class Archer : Entity
{
    public Archer_MoveState MoveState { get; private set; }
    public Archer_IdleState IdleState { get; private set; }
    public Archer_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Archer_MeleeAttackState MeleeAttackState { get; private set; }
    public Archer_LookForPlayerState LookForPlayerState { get; private set; }
    public Archer_StunState StunState { get; private set; }
    public Archer_DeadState DeadState { get; private set; }
    public Archer_DodgeState DodgeState { get; private set; }

    [SerializeField] private D_Move moveStateData;
    [SerializeField] private D_Idle idleStateData;
    [SerializeField] private D_PlayerDetected playedDetectedStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_Stun stunStateData;
    [SerializeField] private D_Dead deadStateData;
    public D_Dodge dodgeStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        MoveState = new Archer_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new Archer_IdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new Archer_PlayerDetectedState(this, stateMachine, "playerDetected", playedDetectedStateData, this);
        MeleeAttackState = new Archer_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        LookForPlayerState = new Archer_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        StunState = new Archer_StunState(this, stateMachine, "stun", stunStateData, this);
        DeadState = new Archer_DeadState(this, stateMachine, "dead", deadStateData, this);
        DodgeState = new Archer_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);

        stateMachine.Initialize(IdleState);
    }


    public override void Damage(float amount, float stunAmount, float knockback)
    {
        base.Damage(amount, stunAmount, knockback);

        if (isDead)
        {
            stateMachine.ChangeState(DeadState);
        }
        if (isStunned && stateMachine.currentState != StunState) // Stun State
        {
            stateMachine.ChangeState(StunState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            LookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(LookForPlayerState);
        }
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
