using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int targetScene;
    public string spawnPosName;
    public string objName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (string.IsNullOrEmpty(spawnPosName) || string.IsNullOrEmpty(objName))
            {
                SceneController.Instance.LoadSceneAsync(targetScene);

            }
            else
            {
                SceneController.Instance.LoadSceneAsync(targetScene, objName, spawnPosName);
            }
        }
    }
}
