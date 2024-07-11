using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
  public static PlayerInput instance;

  public InputButton Pause = new InputButton(KeyCode.Escape);
  public InputButton Attack = new InputButton(KeyCode.J);
  public InputButton Shoot = new InputButton(KeyCode.U);
  public InputButton Jump = new InputButton(KeyCode.K);

  public InputAxis Horizontal = new InputAxis(KeyCode.A, KeyCode.D);
  public InputAxis Vertical = new InputAxis(KeyCode.S, KeyCode.W);

  public bool isInputEnabled = true;

  #region periodic methods
  private void Awake()
  {
    if (instance != null)
    {
      throw new System.Exception("Instance already existed");
    }

    instance = this;
  }

  private void Update()
  {
    if (isInputEnabled)
    {
      Pause.Get();
      Attack.Get();
      Shoot.Get();
      Jump.Get();
      Horizontal.Get();
      Vertical.Get();
    }

  }
  #endregion

  #region  input event handlers

  #endregion

  public void SetEnableInput(bool enable)
  {
    isInputEnabled = enable;
  }
}