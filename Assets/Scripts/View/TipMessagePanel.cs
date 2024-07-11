using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TipStyle
{
    Style1 = 0,// located at the bottom of the screen
    Style2, //
    Style3, //
}
public class TipMessagePanel : SingletonView<TipMessagePanel>
{
    #region fields
    //instructor said this UI would be used in several places, so it should be deigned as a singleton
    GameObject styleObj_1;
    GameObject styleObj_2;
    GameObject styleObj_3;

    #endregion

    #region periodic methods

    protected override void Awake()
    {
        base.Awake();
        styleObj_1 = transform.Find("Style_Down").gameObject;
        styleObj_1.SetActive(false);
        styleObj_2 = transform.Find("Style_2").gameObject;
        styleObj_2.SetActive(false);
        styleObj_3 = transform.Find("Style_3").gameObject;
        styleObj_3.SetActive(false);
    }


    #endregion
    public void ShowTip(string content, TipStyle style = TipStyle.Style1)
    {
        switch (style)
        {
            case TipStyle.Style1:
                styleObj_1.SetActive(true);
                styleObj_1.transform.Find("Content").GetComponent<Text>().text = content;
                break;
            case TipStyle.Style2:
                styleObj_2.SetActive(true);
                Invoke("HideStyle2", 1.5f);
                break;
            case TipStyle.Style3:
                styleObj_3.SetActive(true);
                Invoke("HideStyle3", 1.5f);
                break;
        }
    }

    public void HideTip(TipStyle style = TipStyle.Style1)
    {
        switch (style)
        {
            case TipStyle.Style1:
                styleObj_1.SetActive(false);
                // styleObj_1.transform.Find("Content").GetComponent<Text>().text = "";
                break;
            case TipStyle.Style2:
                styleObj_2.SetActive(false);
                break;
            case TipStyle.Style3:
                styleObj_3.SetActive(false);
                break;
        }
    }

    private void HideStyle2()
    {
        HideTip(TipStyle.Style2);
    }
    private void HideStyle3()
    {
        HideTip(TipStyle.Style3);
    }
}
