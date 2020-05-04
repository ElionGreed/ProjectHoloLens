using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomInfo
{
    public string Name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;
    string currentWorldName = "Start";
    RoomInfo CurrentLoadRoomInformation;

    Queue<RoomInfo> LoadRoomQueue = new Queue<RoomInfo>();
    public List<Room> LoadRooms = new List<Room>();
    bool isRoomLoaded = false;


    void Awake()
    {
        instance = this;
    }

    void UpdateRoomQueue()
    {
        if(isRoomLoaded)
        {
            return;
        }

        if(LoadRoomQueue.Count == 0)
        {
            return;
        }

        CurrentLoadRoomInformation = LoadRoomQueue.Dequeue();
        isRoomLoaded = true;

        StartCoroutine(LoadRoomRoutine(CurrentLoadRoomInformation));

    }

    public bool RoomExist(int x, int y)
    {
        return LoadRooms.Find(item => item.x == x && item.y == y) != null;
    }


    public void LoadRoom(string name, int x, int y)
    {
        if(RoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        LoadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string RoomName = currentWorldName + info.Name;
        AsyncOperation LoadRooms = SceneManager.LoadSceneAsync(RoomName, LoadSceneMode.Additive);

        while(LoadRooms.isDone == false)
        {
            yield return null;
        }
    }


    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3 (CurrentLoadRoomInformation.X * room.width, CurrentLoadRoomInformation.Y * room.Height, 0);


        room.x = CurrentLoadRoomInformation.X;
        room.y = CurrentLoadRoomInformation.Y;

        room.name = currentWorldName + "-" + CurrentLoadRoomInformation.Name + " " + room.x + "," + room.y;
        room.transform.parent = transform;

        isRoomLoaded = false;

        LoadRooms.Add(room);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }
}
