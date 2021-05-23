﻿using UnityEngine;

public class Dingus : Entity
{
    public Dingus_IdleState idleState { get; private set; }
    public Dingus_MoveState moveState { get; private set; }
    public Dingus_PlayerDetectedState playerDetectedState { get; private set; }
    public Dingus_ChargeState chargeState { get; private set; }
    public Dingus_LookForPlayerState lookForPlayerState { get; private set; }
    public Dingus_MeleeAttackState meleeAttackState { get; private set; }
    public Dingus_StunState stunState { get; private set; }

    [SerializeField] private D_Idle idleStateData;
    [SerializeField] private D_Move moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_Charge chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_Stun stunStateData;

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

        stateMachine.Initialize(moveState);
    }


    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        // unique state logic
        if (isStunned && stateMachine.currentState != stunState) // Stun State
        {
            stateMachine.ChangeState(stunState);
        }
    }


    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
