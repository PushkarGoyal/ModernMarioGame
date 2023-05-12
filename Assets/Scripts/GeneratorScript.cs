using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    float screenWidthInPoints;
    public GameObject[] availableRooms; // Available Rooms Array
    public List<GameObject> currentRooms;// Current Allocated Rooms List



    void Start()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
    }

    // Method To Add Room 

    void AddRoom(float LastRoomEndX)
    {

        int randomRoomIndex = Random.Range(0, availableRooms.Length); // Generating Random Room Index
        GameObject room = Instantiate(availableRooms[randomRoomIndex]); // Instantiating Room Based On RandomIndex
        float roomWidth = room.transform.Find("Floor").localScale.x;
        float roomCenter = LastRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0f, 0f);
        currentRooms.Add(room);

    }

    // Method To Check Room Requirement And Destroying Unnecessary Rooms

    void GenerateRoomPerRequirement()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;

        float playerX = transform.position.x;

        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;

        float farthestRoomEndX = 0f;

        foreach (var room in currentRooms)
        {
            float roomWidth = room.transform.Find("Floor").localScale.x;
            float roomStart = room.transform.position.x - (roomWidth * 0.5f);
            float roomEnd = roomStart + roomWidth;

            if (roomStart > addRoomX)
            {
                addRooms = false;
            }
            if (roomEnd < removeRoomX)
                roomsToRemove.Add(room);
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEnd);

        }

        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }

        if (addRooms)
            AddRoom(farthestRoomEndX);

    }

    // Coroutine To Handle Room Creation And Destroying

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomPerRequirement();
            yield return new WaitForSeconds(0.25f);
        }
    }

}
