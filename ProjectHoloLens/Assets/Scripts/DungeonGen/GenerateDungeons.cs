using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDungeons : MonoBehaviour
{

    public RoomScripts startRoomPrefab, endRoomPrefab;
    public List<RoomScripts> roomprefabs = new List<RoomScripts>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<DoorScript> availableDoorways = new List<DoorScript>();

    RoomScripts startRoom;
    RoomScripts endRoom;

    List<RoomScripts> placedRooms = new List<RoomScripts>();

    LayerMask RoomLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        RoomLayerMask = LayerMask.GetMask("Rooms");
        StartCoroutine("GenerateDungeon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator GenerateDungeon()
    {
        WaitForSeconds startUp = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startUp;

        PlaceStartRoom();
        yield return interval;


        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);
        for (int i = 0; i < iterations; i++)
        {
            PlaceRooms();
            yield return interval;
        }

        PlaceEndRoom();
        yield return interval;

        yield return new WaitForSeconds(5);
        ResetDungeonGenerate();
       
    }

    void ResetDungeonGenerate()
    {
        StopCoroutine("GenerateDungeon");

        if (startRoom)
        {
            Destroy(startRoom.gameObject);
        }
        if (endRoom)
        {
            Destroy(endRoom.gameObject);
        }

        foreach (RoomScripts room in placedRooms)
        {
            Destroy(room.gameObject);
        }
        placedRooms.Clear();
        availableDoorways.Clear();

        StartCoroutine("GenerateDungeon");
    }

    void PlaceStartRoom()
    {
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;


        AddDoorwayToList(startRoom, ref availableDoorways);

        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }

    void AddDoorwayToList(RoomScripts room, ref List<DoorScript> list)
    {
        foreach (DoorScript doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }

    void PlaceEndRoom()
    {
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        //List<DoorScript> AllavailableDoorways = new List<DoorScript>(availableDoorways);
        //DoorScript doorway = endRoom.doorways[0];
        //bool roomPlaced = false;

        //foreach (DoorScript availableDoorway in AllavailableDoorways)
        //{
        //    RoomScripts room = (RoomScripts)endRoom;
        //    {
        //        PositionRoomAtDoorway(ref room, doorway, availableDoorway);

        //        if (RoomOverlap(endRoom))
        //        {
        //            continue;
        //        }

        //        roomPlaced = true;
        //        placedRooms.Add(endRoom);
        //        doorway.gameObject.SetActive(false);
        //        availableDoorways.Remove(doorway);

        //        availableDoorway.gameObject.SetActive(false);
        //        availableDoorways.Remove(availableDoorway);

        //        break;
        //    }
           
        //}

        //if (!roomPlaced)
        //{
        //    ResetDungeonGenerate();
        //}

    }

    void PlaceRooms()
    {
        RoomScripts currentRoom = Instantiate(roomprefabs[Random.Range(0, roomprefabs.Count)]) as RoomScripts;
        currentRoom.transform.parent = this.transform;

        List<DoorScript> AllavailableDoorways = new List<DoorScript>(availableDoorways);
        List<DoorScript> CurrentRoomDoorways = new List<DoorScript>();
        AddDoorwayToList(currentRoom, ref CurrentRoomDoorways);

        AddDoorwayToList(currentRoom, ref availableDoorways);
        bool roomPlaced = false;

        foreach (DoorScript availableDoorway in AllavailableDoorways)
        {
            foreach (DoorScript currentDoorway in CurrentRoomDoorways)
            {
                PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

                if (RoomOverlap(currentRoom))
                {
                    continue;
                }

                roomPlaced = true;
                placedRooms.Add(currentRoom);
                currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);

                break;
            }

            if (roomPlaced)
            {
                break;
            }
        }
             if (!roomPlaced)
             {
            Destroy(currentRoom.gameObject);
            ResetDungeonGenerate();
             }
    }

    void PositionRoomAtDoorway(ref RoomScripts room, DoorScript RoomDoorway, DoorScript target)
    {
        room.transform.position = Vector3.zero;
        room.transform.rotation = Quaternion.identity;

        Vector3 targetRule = target.transform.eulerAngles;
        Vector3 RoomDoorwaysRule = RoomDoorway.transform.eulerAngles;
        float Angle = Mathf.DeltaAngle(RoomDoorwaysRule.y, targetRule.y);
        Quaternion currentroomtargetRo = Quaternion.AngleAxis(Angle, Vector3.up);
        room.transform.rotation = currentroomtargetRo * Quaternion.Euler(0, 180f, 0);


        Vector3 roomOffest = RoomDoorway.transform.position - room.transform.position;
        room.transform.position = target.transform.position - roomOffest;
    }

    bool RoomOverlap(RoomScripts room)
    {
        Bounds bounds = room.RoomBounds;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 4, room.transform.rotation, RoomLayerMask);

        if(colliders.Length > 0)
        {
            foreach(Collider c in colliders)
            {
                if(c.transform.parent.gameObject.Equals(room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Error");
                    return true;
                }
            }
        }
        return false;
    }

   
}
