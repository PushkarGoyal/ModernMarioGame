using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxCamera : MonoBehaviour
{
    [SerializeField] float parallaxEffect;

    Vector3 lastCameraPosition;
    Vector3 lastPlayerPosition;

    float roomStartX;

    GameObject parentRoom, player;
    float roomWidth;

    float roomEndX;





    private void Start()
    {
        lastCameraPosition = Camera.main.transform.position;

        parentRoom = transform.parent.gameObject;
        roomWidth = parentRoom.transform.Find("Floor").localScale.x;
        roomStartX = parentRoom.transform.position.x - (roomWidth * 0.5f);
        roomEndX = roomStartX + roomWidth;
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerPosition = player.transform.position;



    }

    private void LateUpdate()
    {

        if (lastPlayerPosition.x > roomStartX)
        {


            Vector3 deltaMovement = Camera.main.transform.position - lastCameraPosition;

            transform.position += deltaMovement * parallaxEffect;



            //Vector3 deltaPMovement = player.transform.position - lastPlayerPosition;
            //deltaPMovement.y = 0f;
            //transform.position += deltaPMovement * parallaxEffect;

            lastCameraPosition = Camera.main.transform.position;
            lastPlayerPosition = player.transform.position;
        }
        else
        {

        }






    }


}
