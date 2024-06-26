using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
  public Transform target;

  public Vector2 rangeMin, rangeMax;

  private void Update()
  {
    Follow();
  }
  public void Follow()
  {
    if (target == null)
    {
      Debug.LogWarning(transform.name + " the target is null");
      return;
    }
    //notice the z coord of the camera cannot be changed
    Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

    transform.position = LimitPosition(targetPos); ;
  }

  private Vector3 LimitPosition(Vector3 targetPos)
  {
    if (targetPos.x < rangeMin.x) { targetPos.x = rangeMin.x; }
    if (targetPos.x > rangeMax.x) { targetPos.x = rangeMax.x; }
    if (targetPos.y < rangeMin.y) { targetPos.y = rangeMin.y; }
    if (targetPos.y > rangeMax.y) { targetPos.y = rangeMax.y; }
    return targetPos;
  }
}