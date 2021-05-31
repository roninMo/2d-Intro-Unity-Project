using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    private float velocityToSet;
    private bool setVelocity;
    private bool shouldCheckFlip;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string currentAnimation) : base(player, stateMachine, playerData, currentAnimation)
    {
    }


    public override void Enter()
    {
        base.Enter();
        setVelocity = false;
        weapon.EnterWeapon();
    }


    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (shouldCheckFlip)
        {
            Core.Movement.CheckIfShouldFlip(input.x);
        }
        if (setVelocity)
        {
            Core.Movement.SetVelocityX(velocityToSet * Core.Movement.FacingDirection);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    public override void DoChecks()
    {
        base.DoChecks();
    }


    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this); // This links the state to the weapon, which links the animation trigger finish together, so it knows when to stop attacking
    }


    public void SetPlayerVelocity(float velocity)
    {
        Core.Movement.SetVelocityX(velocity * Core.Movement.FacingDirection);
        velocityToSet = velocity;
        setVelocity = true;
    }


    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }


    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }
}
