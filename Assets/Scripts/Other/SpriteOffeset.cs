using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOffeset : MonoBehaviour
{
    Vector3 startPosition;
    public float offsetNumber = 0.0001f;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = startPosition + Input.mousePosition * offsetNumber;
    }
}
