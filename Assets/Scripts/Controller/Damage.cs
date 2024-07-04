using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;

    public void OnDamage(GameObject[] objects)
    {
        foreach (var obj in objects)
        {
            OnDamage(obj);
        }
    }
    public void OnDamage(GameObject obj)
    {

        Damageable damageable = obj.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
