using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButtons : MonoBehaviour
{
    private Transform triangle;

    private Image btn_image;
    private Sprite normalSprite;
    public Sprite highlightedSprite;
    // Start is called before the first frame update
    void Start()
    {
        triangle = transform.Find("Triangle");
        triangle.gameObject.SetActive(false);
        btn_image = transform.GetComponent<Image>();
        normalSprite = btn_image.sprite;
    }

    #region event handlers
    public void OnPointerEnter()
    {
        SetHighlighted(true);
    }

    //call this function when the mouse leaves the button or when the mouse button is released, causing the button exit the highlighted state.
    public void OnPointerExit()
    {
        SetHighlighted(false);
    }

    public void OnPointerUp()
    {
        SetHighlighted(false);
    }

    private void SetHighlighted(bool isLighted)
    {
        triangle.gameObject.SetActive(isLighted);
        btn_image.sprite = isLighted ? highlightedSprite : normalSprite;
    }
    #endregion
}
