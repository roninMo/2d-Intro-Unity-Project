using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dingus : Entity
{
    public Dingus_IdleState idleState { get; private set; }
    public Dingus_MoveState moveState { get; private set; }
    public Dingus_PlayerDetectedState playerDetectedState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;

    public override void Start()
    {
        base.Start();

        moveState = new Dingus_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Dingus_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Dingus_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);

        stateMachine.Initialize(moveState);
    }
}
