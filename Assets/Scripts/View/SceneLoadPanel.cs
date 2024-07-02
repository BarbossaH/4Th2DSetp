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
    // Debug.Log(999);
    slider_process.value = process;
    if (process >= 1)
    {
      // this.Hide();
      Invoke("Hide", 0.5f);
    }
  }
  public void UpdateLoadProcess(AsyncOperation operation)
  {
    Show();
    currentOperation = operation;

  }

  public override void Hide()
  {
    base.Hide();
    slider_process.value = 0;
    currentOperation = null;
  }
}
