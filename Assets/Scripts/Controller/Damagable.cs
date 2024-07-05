using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    //this script is for checking if the object collided with another object that can cause damage

    public int health;

    public Action OnGetHurt;
    public Action OnDie;

    public void TakeDamage(int damage)
    {
        /* Here, many game objects will mount this script, such as player, pillars, walls, and monsters,
        so this script cannot tell which object calls this method until it's called. So we need to use
        event handlers to deal with this situation.
        */
        //reduce the health value by the damage
        health--;
        if (health <= 0)
        {
            OnDie?.Invoke();
            // if (onDie != null)
            // {
            //     onDie();
            // }
        }
        else
        {
            OnGetHurt?.Invoke();
            // if (OnGetHurt != null)
            // {
            //     OnGetHurt();
            // }
        }
        //play the injury animation

        //check if the object is dead when its health is lower than 0
    }
}
