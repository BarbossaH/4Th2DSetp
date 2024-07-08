1. Most classes have access to some kind of minion.

2. On the skill screen, you can pick the composition of your army.

3. This is a level two area, so I can only afford one other minion. I will pick a sniper for now.

4 when minions die, they'll be revived automatically so long as no other minion has died recently.

5. But they're fairly tough and can bolster your minion numbers.

6. I am going to summon a skeletal Arsonist. They can throw little fire grenades, but they also have commands skill that can detonate your own minions that are low on life.

7. when you see the red indicator, use the skill and the minion will go boom.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePaneL : SingletonView<GamePaneL>
{

    public GameObject hp_prefab;
    Transform parent;
    GameObject[] hp_icons;
    public int HP;
    // in real project ,we won't set a UI as singleton.

    protected override void Awake()
    {
        base.Awake();
        parent = transform.Find("HP");

    }

    public void InitHP(int hp)
    {
        hp_icons = new GameObject[hp];
        for (int i = 0; i < hp; i++)
        {
            hp_icons[i] = GameObject.Instantiate(hp_prefab, parent);
        }
    }

    public void UpdateHp(int hp)
    {
        // Debug.Log(hp);
        if (hp <= 0) hp = 0;
        for (int i = hp; i < hp_icons.Length; i++)
        {
            if (hp_icons[i].GetComponent<Toggle>().isOn)
            {
                hp_icons[i].GetComponent<Toggle>().isOn = false;
                //to do: play an animation or effect
            }
        }
    }
}
