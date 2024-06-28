using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoadPanel : SingletonView<SceneLoadPanel>
{
  private Slider slider_process;
  private AsyncOperation currentOperation;
  private void Start()
  {
    // Slider slider_process1 = transform.Find("Slider").GetComponent<Slider>();
    slider_process = transform.GetComponentInChildren<Slider>();
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
      this.Hide();
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
