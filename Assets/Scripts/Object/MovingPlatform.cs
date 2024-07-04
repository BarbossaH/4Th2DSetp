using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public float speed = 1f;

    public bool isMoveToEnd = true;

    private Rigidbody2D rb;
    public ContactFilter2D contactFilter;
    ContactPoint2D[] contactPoints = new ContactPoint2D[10];

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FollowObject()
    {
        int count = rb.GetContacts(contactFilter, contactPoints);
        for (int i = 0; i < count; i++)
        {
            contactPoints[i].rigidbody.velocity += new Vector2(isMoveToEnd ? speed : -speed, 0);
        }
    }

    private void Update()
    {
        if (isMoveToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
            if (transform.position == endPos)
            {
                isMoveToEnd = false;
            }
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            if (transform.position == startPos)
            {
                isMoveToEnd = true;
            }
        }
    }

    private void LateUpdate()
    {
        //because the player's movement is controlled in the update method, when there is no input event, it means the player won't move. so if FollowObject method is called in the update method, it won't work because the player's velocity will be covered by SetSpeedX method. So the velocity of the player is still 0.
        FollowObject();
    }
}
