using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OderSetting : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public int orderInlayer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = orderInlayer;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
