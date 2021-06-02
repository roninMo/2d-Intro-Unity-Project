using UnityEngine;

public class Dingus_DamageReceiver : MonoBehaviour, IDamageable
{
    [HideInInspector] public Entity entity;


    public void Damage(float amount, float stunAmount, float knockback)
    {
        Debug.Log("Damage dealt to Dingus: " + amount);
        Debug.Log("Knockback dealt to Dingus" + knockback);
        entity.Damage(amount, stunAmount, knockback);
    }
}
