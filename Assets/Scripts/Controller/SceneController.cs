using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController
{
  private static SceneController _instance;

  private SceneController()
  {
  }
  public static SceneController Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new SceneController();
      }

      return _instance;
    }
  }

  AsyncOperation operation = null;
  public void LoadSceneAsync(int targetIndex)
  {

    //then load the scene
    operation = SceneManager.LoadSceneAsync(targetIndex);
    //show a loading interface
    SceneLoadPanel.Instance.UpdateLoadProcess(operation);

  }
}