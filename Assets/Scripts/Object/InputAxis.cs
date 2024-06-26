using UnityEngine;

public class InputAxis
{
  public KeyCode k1;
  public KeyCode k2;

  public float value;

  public InputAxis(KeyCode k1, KeyCode k2)
  {
    this.k1 = k1;
    this.k2 = k2;
  }

  public void Get()
  {
    if (Input.GetKey(k1))
    {
      value = -1; //left or down
      return;
    }
    if (Input.GetKey(k2))
    {
      value = 1; //right or up
      return;
    }
    value = 0; //no input
  }
}