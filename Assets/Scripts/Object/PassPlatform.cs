using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlatform : MonoBehaviour
{
    // public GameObject test;
    PlatformEffector2D effector;
    private int layer;
    private void Start()
    {
        effector = transform.GetComponent<PlatformEffector2D>();
    }
    public void Fall(GameObject target)
    {
        //取消碰撞层
        layer = 1 << target.layer;
        // Debug.Log($"this layer is {layer}");
        effector.colliderMask &= ~layer;
        //恢复碰撞层
        Invoke("ResetLayer", 0.5f);
    }

    public void ResetLayer()
    {
        effector.colliderMask |= layer;
    }
}
