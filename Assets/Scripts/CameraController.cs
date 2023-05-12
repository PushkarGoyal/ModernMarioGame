using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player; // Reference To Player Gameobject
    Vector3 CameraPosition; // Camera Position Vector
    Vector3 playerPosition; // PlayerPosition Vector

    public static float offset;

    //float bgSpeed = 0.3f;
    //float fgSpeed = 0.6f;

    //[SerializeField] GameObject Background;
    //[SerializeField] GameObject Foreground;



    void Start()
    {
        CameraPosition = Camera.main.transform.position; // Getting Main Camera Position

        playerPosition = player.transform.position; // Getting Player Position

    }


    void Update()
    {

        offset = CameraPosition.x - playerPosition.x; // Finding offset B/W Player And Camera Position
        float change = player.transform.position.x + offset; // Calculating change To Miantain Offset


        Vector3 CamPos = transform.position;
        change = Mathf.Clamp(change, CamPos.x, float.PositiveInfinity); // Clamping Camera Position Bw Camera Last Position and Infinity

        CamPos.x = change;
        transform.position = CamPos; // Setting cameera Position To new Position




    }


}
