using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuPanel : ViewBase
{
    #region Fields
    public OptionPanel optionPanel;
    #endregion


    #region Click Events
    public void OnStartGameClick()
    {
        //TODO
        SceneController.Instance.LoadSceneAsync(1);
    }
    public void OnOptionClick()
    {
        //hide self and show option panel
        this.Hide();
        optionPanel.Show();
    }
    public void OnQuitGameClick()
    {
        // if (Application.isEditor)
        // {
        //     EditorApplication.isPlaying = true;

        // }
        // else
        // {
        //     Application.Quit();
        // }
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
    #endregion
}
