using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
  AsyncOperation operation = null;
  public void LoadSceneAsync(int targetIndex)
  {
    if (SceneLoadPanel.Instance == null)
    {
      GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/View/LoadScenePanel"));
    }
    // then load the scene
    operation = SceneManager.LoadSceneAsync(targetIndex);
    // show a loading interface

    SceneLoadPanel.Instance.UpdateLoadProcess(operation);

  }

  private void LoadSceneAsync(int targetIndex, Action<AsyncOperation> onComplete)
  {
    if (SceneLoadPanel.Instance == null)
    {
      GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/View/LoadScenePanel"));
    }
    // then load the scene
    operation = SceneManager.LoadSceneAsync(targetIndex);
    // show a loading interface

    SceneLoadPanel.Instance.UpdateLoadProcess(operation);
    operation.completed += onComplete;
  }

  public void LoadSceneAsync(int targetIndex, string objName, string posName)
  {
    //这里的风险就是在手动输入对应的objname和posname的时候容易出错。我想不会这么输入或者有一个规范，我这里很乱。
    LoadSceneAsync(targetIndex, (asyncOperation) =>
    {
      GameObject obj = GameObject.Find(objName);
      // Debug.Log(posName);
      GameObject targetObject = GameObject.Find(posName);

      obj.transform.position = targetObject.transform.position;
    });
  }
}