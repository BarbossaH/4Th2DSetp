using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HubDoorStatusEnum
{
    Zero = 0,
    One,
    Two,
    Three,
}
public class HubDoorScript : MonoBehaviour
{
    public Sprite[] doorSprite;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDoorStatus(HubDoorStatusEnum status)
    {
        spriteRenderer.sprite = doorSprite[(int)status];
    }
}
