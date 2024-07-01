using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
  public Transform target;

  public Vector2 rangeMin, rangeMax;

  private bool isFollowWithTime = false;
  private bool isChangeViewSize = false;
  public float movingDuration = 0.0f;
  public float movingTimer = 0;

  public float defaultCameraSize;
  public float cameraSize = 5;
  private Vector3 startPos;

  Camera mainCamera;

  private void Start()
  {
    mainCamera = GetComponent<Camera>();
    defaultCameraSize = mainCamera.orthographicSize;
    // Debug.Log(defaultCameraSize);
  }

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
    Vector3 targetPos;
    if (isFollowWithTime)
    {
      movingTimer += Time.deltaTime;
      targetPos = Vector3.Lerp(startPos, target.position, movingTimer / movingDuration);
      targetPos.z = transform.position.z;
      // Debug.Log($"this is {targetPos}");
    }
    else
    {
      targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    if (isChangeViewSize)
    {
      mainCamera.orthographicSize = Mathf.Lerp(defaultCameraSize, cameraSize, movingTimer / movingDuration);
    }
    if (movingTimer / movingDuration >= 1)
    {
      isFollowWithTime = false;
      isChangeViewSize = false;
      movingTimer = 0;
      // mainCamera.orthographicSize = defaultCameraSize;
    }
    transform.position = LimitPosition(targetPos); ;
  }
  public void SetFollowedTarget(Transform target)
  {
    this.target = target;
    isFollowWithTime = false;
    isChangeViewSize = false;
  }

  public void SetFollowedTarget(Transform target, float duration)
  {
    this.target = target;
    startPos = transform.position;
    isFollowWithTime = true;
    movingTimer = 0;
    movingDuration = duration;
    // Debug.Log(movingDuration);
  }

  public void SetFollowedTarget(Transform target, float cameraSize, float duration)
  {
    SetFollowedTarget(target, duration);
    isChangeViewSize = true;
    this.cameraSize = cameraSize;
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