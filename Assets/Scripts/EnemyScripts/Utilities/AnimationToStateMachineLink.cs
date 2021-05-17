using UnityEngine;

public class AnimationToStateMachineLink : MonoBehaviour
{
    public AttackState attackState;

    // This is what links the animator to our trigger functions when they're not on the same object
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    // What's awesome about this is it all the different types of attacks funnel into these trigger attack and finish attack functions, and we only need one of these
    // scripts attached to the game object and it will be able to call these functions from any type of attack be it melee, range, fart, or magic/spellcraft
}
