using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dingus : Entity
{
    public Dingus_IdleState idleState { get; private set; }
    public Dingus_MoveState moveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new Dingus_MoveState(this, stateMachine, "move", moveStateData, this);
        //idleState = new Dingus_IdleState(this, stateMachine, "idle", idleStateData,);
    }
}
