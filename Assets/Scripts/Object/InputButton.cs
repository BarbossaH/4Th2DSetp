using UnityEngine;

public class InputButton
{
  public KeyCode keyCode;
  public bool isDown;
  public bool isUp;
  public bool isHolding;

  public InputButton(KeyCode keyCode)
  {
    this.keyCode = keyCode;
  }

  public void Get()
  {
    isDown = Input.GetKeyDown(keyCode);
    isUp = Input.GetKeyUp(keyCode);
    isHolding = Input.GetKey(keyCode);
  }
}