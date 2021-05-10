﻿using UnityEngine;

public class MoveState : EnemyState
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_MoveState stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }


    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckLedge();
    }


    public override void Exit()
    {
        base.Exit();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckLedge();
    }
}