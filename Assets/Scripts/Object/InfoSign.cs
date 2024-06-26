using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSign : MonoBehaviour
{
    public Sprite normalSprite, highlightedSprite;
    private SpriteRenderer spriteRenderer;
    public string text;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Debug.Log(other.name);
            spriteRenderer.sprite = highlightedSprite;
            TipMessagePanel.Instance.ShowTip(text);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.sprite = normalSprite;
            TipMessagePanel.Instance.HideTip();
        }
    }
}
