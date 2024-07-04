using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Damage damage;
    private void Start()
    {
        damage = GetComponent<Damage>();

    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //damage the player
        damage.OnDamage(col.gameObject);
        //refresh the UI
    }
}
