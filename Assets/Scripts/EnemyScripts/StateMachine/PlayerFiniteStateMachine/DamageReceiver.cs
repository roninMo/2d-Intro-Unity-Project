using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    [HideInInspector] public Entity entity;


    public void Damage(float amount, float stunAmount, float knockback)
    {
        Debug.Log("Damage dealt to Enemy: " + amount);
        Debug.Log("Stun amount dealt to Enemy: " + stunAmount);
        Debug.Log("Knockback dealt to Enemy" + knockback);
        entity.Damage(amount, stunAmount, knockback);
    }
}
