using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerations : MonoBehaviour
{

    public Room startRoomPrefab, endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<Doorway> availableDoorways = new List<Doorway>();
    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();
    LayerMask roomLayerMask;



    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("Generate");
    }

    IEnumerator Generate()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;
        Debug.Log("GodHelpMe");
        //PlaceStartRoom();
        yield return interval;

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for (int i = 0; i < iterations; i++)
        {
            Debug.Log("PleaseGodHelpMe");

            //PlaceRoom();
            yield return interval;
        }

        Debug.Log("EndMePleaseGod");
        yield return interval;


        Debug.Log("Generation Done");
        yield return new WaitForSeconds(3);
        Debug.Log("Reset Done");
        StopCoroutine("Generate");
        StartCoroutine("Generate");
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
