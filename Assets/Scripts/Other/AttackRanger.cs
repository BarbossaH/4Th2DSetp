using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRanger : MonoBehaviour
{
    //this script is for getting all objects that can be attacked
    List<GameObject> damageables = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageables.Add(damageable.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null && !damageables.Contains(damageable.gameObject))
        {
            damageables.Add(damageable.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageables.Remove(damageable.gameObject);
        }
    }

    public GameObject[] GetDamageables()
    {
        return damageables.ToArray();
    }
}
