using UnityEngine;

public class Dingus : Entity
{
    public Dingus_IdleState idleState { get; private set; }
    public Dingus_MoveState moveState { get; private set; }
    public Dingus_PlayerDetectedState playerDetectedState { get; private set; }
    public Dingus_ChargeState chargeState { get; private set; }
    public Dingus_LookForPlayerState lookForPlayerState { get; private set; }
    public Dingus_MeleeAttackState meleeAttackState { get; private set; }
    public Dingus_StunState stunState { get; private set; }
    public Dingus_DeadState deadState { get; private set; }

    [SerializeField] private D_Idle idleStateData;
    [SerializeField] private D_Move moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_Charge chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_Stun stunStateData;
    [SerializeField] private D_Dead deadStateData;

    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new Dingus_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Dingus_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Dingus_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Dingus_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new Dingus_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new Dingus_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Dingus_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new Dingus_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
    }


    //public override void Damage(AttackDetails attackDetails)
    public override void Damage(float amount, float stunAmount, float knockback)
    {
        base.Damage(amount, stunAmount, knockback);

        // State logic
        if (isDead) // Dead State
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState) // Stun State
        {
            stateMachine.ChangeState(stunState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public void Damage(float amount)
    {
        Debug.Log("Damage dealt to dingus: " + amount);
    }
}
