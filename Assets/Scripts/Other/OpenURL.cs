using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;

    public void Open()
    {
        if (string.IsNullOrEmpty(url))
        {
            return;
        }

        Application.OpenURL(url);
    }
}
