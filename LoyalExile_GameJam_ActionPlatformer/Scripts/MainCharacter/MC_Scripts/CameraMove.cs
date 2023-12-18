using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    Transform cameraTransform;
    Transform playerTransform;
    void Start()
    {
        cameraTransform = GetComponent<Transform>();

        GameObject player = GameObject.Find("Player");

        playerTransform = player.GetComponent<Transform>();

        //Transform[] tranArray = GetComponentsInChildren<Transform>();

        /*foreach (Transform tran in tranArray) 
        {
            if( tran.parent != null)
            {
                playerTransform = tran;
                break;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
    }
}
