using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;


    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }


    // This is the link to the function within the weapon script. The game object normally doesn't have access to the weapon script because it's not attached to the component
    // Player attack state passes in it's state to weapon, and weapon is added to the attack state's current weapon in the constructor, then we have linked functions for the trigger
    // So the weapon state has a function that calls state.animationfinishtrigger which ends the ability(currently attack) which is achieved by linking the stuff together, and
    // With this script it's like a bridge that grabs weapon's function and stores it in this script, and we put this on the object with the animation controller to call it
    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger(); 
    }


    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }


    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }


    private void AnimationTurnOffFlipTrigger()
    {
        weapon.AnimationTurnOffFlipTrigger();
    }


    private void AnimationTurnOnFlipTrigger()
    {
        weapon.AnimationTurnOnFlipTrigger();
    }


    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }
}
