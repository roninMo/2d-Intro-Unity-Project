using UnityEngine;

public class DeadState : EnemyState
{
    protected D_Dead stateData;

    public DeadState(Entity entity, FiniteStateMachine stateMachine, string currentAnimation, D_Dead stateData) : base(entity, stateMachine, currentAnimation)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(stateData.deathBloodParticles, entity.aliveGameObject.transform.position, stateData.deathBloodParticles.transform.rotation);
        GameObject.Instantiate(stateData.deathChunkParticles, entity.aliveGameObject.transform.position, stateData.deathChunkParticles.transform.rotation);

        entity.gameObject.SetActive(false);
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
}
