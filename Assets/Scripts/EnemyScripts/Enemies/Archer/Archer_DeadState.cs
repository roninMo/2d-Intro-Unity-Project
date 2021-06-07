using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DeadState : DeadState
{
    private Archer enemy;

    public Archer_DeadState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Dead stateData, Archer enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
