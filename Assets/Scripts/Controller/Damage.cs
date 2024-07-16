using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HurtType
{
    Normal = 0,
    Deadly
}

public class Damage : MonoBehaviour
{
    public int damage;
    public HurtType hurtType;
    public string resetPosition;
    public void OnDamage(GameObject[] objects)
    {
        if (objects != null && objects.Length > 0)
        {
            foreach (var obj in objects)
            {
                OnDamage(obj);
            }
        }

    }
    public void OnDamage(GameObject obj)
    {

        Damageable damageable = obj.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, hurtType, resetPosition);
        }
    }
}
