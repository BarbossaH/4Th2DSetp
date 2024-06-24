using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : ViewBase
{
    public MenuPanel menuPanel;

    public OperationBG operationBG;
    public AudioSetting audioSetting;

    private GameObject btn_sound;
    private GameObject btn_operation;
    private GameObject information;
    // Btn_Sound
    // Btn_Operation
    // Information
    #region Periodic functions
    private void Start()
    {
        btn_sound = transform.Find("OptionBG/Btn_Sound").gameObject;
        btn_operation = transform.Find("OptionBG/Btn_Operation").gameObject;
        information = transform.Find("OptionBG/Information").gameObject;
    }
    #endregion


    #region Click Events

    public void OnAudioClick()
    {
        SetChildrenActive(false);
        audioSetting.Show();
    }
    public void OnOperationClick()
    {
        SetChildrenActive(false);
        operationBG.Show();
    }

    public void OnBackClick()
    {
        //if audio setting and operation UI are activated, it should response to close them first.
        if (operationBG.IsShown() || audioSetting.IsShown())
        {
            operationBG.Hide();
            audioSetting.Hide();
            SetChildrenActive(true);
        }
        else
        {
            this.Hide();
            menuPanel.Show();
        }
    }
    #endregion

    #region Logic Functions
    private void SetChildrenActive(bool active)
    {
        btn_sound.SetActive(active);
        btn_operation.SetActive(active);
        information.SetActive(active);
    }
    #endregion
}
