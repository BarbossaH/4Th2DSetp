using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : ViewBase
{
    #region fields
    public Slider slider_music;
    public Slider slider_sfx;
    #endregion


    #region event handlers

    //save the value of music slider 
    public void OnMusicValueChanged(float f)
    {
        PlayerPrefs.SetFloat(Constants.MusicVolume, f);
        //todo: change the volume of the music
    }
    //save the value of sfx slider
    public void OnSfxValueChanged(float f)
    {
        PlayerPrefs.SetFloat(Constants.SfxVolume, f);
        //todo: change the volume of the sfx

    }
    #endregion
    #region show and hide
    public override void Show()
    {
        base.Show();
        //read the values from the device
        slider_music.value = PlayerPrefs.GetFloat(Constants.MusicVolume, 0.5f);
        slider_sfx.value = PlayerPrefs.GetFloat(Constants.SfxVolume, 0.5f);
    }

    public override void Hide()
    {
        base.Hide();
        //save the values to the device
    }
    #endregion
}
