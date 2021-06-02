using UnityEngine;

public class Dingus_DeadState : DeadState
{
    private Dingus enemy;

    public Dingus_DeadState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Dead stateData, Dingus enemy) : base(entity, stateMachine, currentAnimation, stateData)
    {
        this.enemy = enemy;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
