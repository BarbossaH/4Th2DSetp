using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TipStyle
{
    Style1 = 0,// located at the bottom of the screen
}
public class TipMessagePanel : MonoBehaviour
{
    #region fields
    //instructor said this UI would be used in several places, so it should be deigned as a singleton
    public static TipMessagePanel _instance;
    GameObject styleObj_1;
    #endregion

    #region periodic methods
    private void Awake()
    {
        //because the game object is set to be active in the editor, but in the code, it is set to inactive. So actually, this Awake method is called first when it is set to inactive in start method.
        _instance = this;
    }
    private void Start()
    {
        styleObj_1 = transform.Find("Style_Down").gameObject;
        styleObj_1.SetActive(false);
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
        }
    }
}
