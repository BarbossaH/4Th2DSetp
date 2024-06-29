using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadPanel : SingletonView<SceneLoadPanel>
{
  private Slider slider_process;
  private AsyncOperation currentOperation;
  protected override void Awake()
  {
    base.Awake();
    DontDestroyOnLoad(gameObject);
  }
  private void Start()
  {

    slider_process = transform.Find("bg/Slider").GetComponent<Slider>();
    // Debug.Log(slider_process1 == slider_process); //true
  }
  private void Update()
  {
    if (currentOperation != null)
    {
      UpdateLoadProcess(currentOperation.progress);
    }

  }
  private void UpdateLoadProcess(float process)
  {
    Show();
    slider_process.value = process;
    if (process >= 1)
    {
      // this.Hide();
      Invoke("Hide", 1);
    }
  }
  public void UpdateLoadProcess(AsyncOperation operation)
  {
    currentOperation = operation;
  }

  public override void Hide()
  {
    base.Hide();
    currentOperation = null;
  }
}
