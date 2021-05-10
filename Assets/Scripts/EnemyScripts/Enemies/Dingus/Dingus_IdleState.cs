﻿using UnityEngine;

public class Dingus_IdleState : IdleState
{
    private Dingus enemy;

    public Dingus_IdleState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_IdleState stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void Enter()
    {
        base.Enter();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // State logic
        if (isIdleTimeOver) // Move State
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}