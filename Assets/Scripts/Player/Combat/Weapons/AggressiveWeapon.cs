using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggresiveWeaponData aggresiveWeaponData;
    private List<IDamageable> detectedDamageable = new List<IDamageable>();


    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_AggresiveWeaponData))
        {
            aggresiveWeaponData = (SO_AggresiveWeaponData)weaponData;
        }
        else
        {
            Debug.Log("Wrong data for the weapon");
        }
    }


    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }


    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggresiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageable)
        {
            item.Damage(details.damageAmount, details.stunDamageAmount, details.knockbackForce);
        }
    }


    public void AddToDetected(Collider2D collision)
    {
        // If this component has an Idamageable component, then add it to the list
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }


    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }
}
