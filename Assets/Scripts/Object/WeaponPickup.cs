using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private Sprite withWeapon, noWeapon;

    private SpriteRenderer spriteRenderer;

    // bool isHavingWeapon = true;
    // this variable is used for checking if the weapon is having or not.but when the new scene is loaded, the objects in the current scene will be destroyed. And if the player come back to the scene again, this object will be created again and the variables will be initialized again, which means the value will be restored even though the player has taken the weapon. And the value is also suitable for storing in the hardware,because when users restart the game, the value should be restored. So the value should be saved in the memory. If the game keeps going, the value keeps being what it should be until the game is over.

    private void Start()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        InitData();
    }
    private void InitData()
    {
        Data<bool> data = DataManager.Instance.GetData(DataConstraints.IsHavingWeapon) as Data<bool>;
        if (data != null)
        {
            spriteRenderer.sprite = data.value ? withWeapon : noWeapon;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == TagConstants.Player)
        {
            DataManager.Instance.SaveData(DataConstraints.IsHavingWeapon, new Data<bool>() { value = false });
            spriteRenderer.sprite = noWeapon;

            TipMessagePanel.Instance.ShowTip("You now have a weapon", TipStyle.Style1);

            Invoke("HideTip", 2f);

            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void HideTip()
    {
        TipMessagePanel.Instance.HideTip();
    }
}
