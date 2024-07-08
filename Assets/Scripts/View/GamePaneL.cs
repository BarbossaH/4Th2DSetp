using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : SingletonView<GamePanel>
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
