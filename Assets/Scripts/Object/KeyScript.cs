using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public static int KeyCount = 0;
    GameObject door;
    HubDoorScript hubDoorScript;
    GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == TagConstants.Player)
        {
            //get the key
            KeyCount++;
            //get the target followed by camera, which is the door.
            door = GameObject.Find("HubDoor");
            if (door != null)
            {
                //cannot manipulate the player
                PlayerInput.instance.SetEnableInput(false);
                hubDoorScript = door.GetComponent<HubDoorScript>();
                Camera.main.GetComponent<CameraFollowTarget>().SetFollowedTarget(door.transform, 3.5f, 1f);
                // Debug.Log(door.transform.position);

                //focus on the door
                //change the state of the door
                Invoke("ChangeDoorState", 1.5f);
                //reset the camera after 2 seconds.and destroy the key object
                Invoke("ResetToNormalState", 2.0f);
            }
            else
            {
                throw new System.Exception("Could not find the door");
            }

            //fresh the UI


            gameObject.SetActive(false);

        }
    }

    private void ChangeDoorState()
    {
        hubDoorScript.SetDoorStatus((HubDoorStatusEnum)KeyCount);

    }

    private void ResetToNormalState()
    {
        //recover to control the player

        PlayerInput.instance.SetEnableInput(true);
        Camera.main.GetComponent<CameraFollowTarget>().SetFollowedTarget(GameObject.Find("Player/TargetForCamera").transform, 5f, 1f);

        Destroy(gameObject);
    }
}
