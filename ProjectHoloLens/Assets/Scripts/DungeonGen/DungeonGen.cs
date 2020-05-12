using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{
	public RoomScript startRoomPrefab, endRoomPrefab;
	public List<RoomScript> roomPrefabs = new List<RoomScript>();
	public Vector2 iterationRange = new Vector2(3, 10);
	public GameObject playerPrefab;
	public GameObject CompanionVariant;

	List<ADoorways> avDoorways = new List<ADoorways>();

	StartRoom startRoom;
	EndRoom endRoom;
	List<RoomScript> placedRooms = new List<RoomScript>();

	LayerMask roomLayerMask;

	GameObject player;
	GameObject Companion;

	void Start()
	{
		roomLayerMask = LayerMask.GetMask("Room");
		StartCoroutine("GenerateLevel");
	}

	IEnumerator GenerateLevel()
	{
		WaitForSeconds startup = new WaitForSeconds(1);
		WaitForFixedUpdate interval = new WaitForFixedUpdate();

		yield return startup;
		PlaceStartRoom();
		yield return interval;
		int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

		for (int i = 0; i < iterations; i++)
		{
			PlaceRoom();
			yield return interval;
		}

		PlaceEndRoom();
		yield return interval;
		player = Instantiate(playerPrefab);
		player.transform.position = startRoom.playerStart.position;
		player.transform.rotation = startRoom.playerStart.rotation;

		Companion = Instantiate(CompanionVariant);
		Companion.transform.position = startRoom.playerStart.position;
		Companion.transform.rotation = startRoom.playerStart.rotation;
	}

	void PlaceStartRoom()
	{
		startRoom = Instantiate(startRoomPrefab) as StartRoom;
		startRoom.transform.parent = this.transform;
		AddDoorwaysToList(startRoom, ref avDoorways);
		startRoom.transform.position = Vector3.zero;
		startRoom.transform.rotation = Quaternion.identity;
	}

	void AddDoorwaysToList(RoomScript room, ref List<ADoorways> list)
	{
		foreach (ADoorways doorway in room.doorways)
		{
			int r = Random.Range(0, list.Count);
			list.Insert(r, doorway);
		}
	}

	void PlaceRoom()
	{
		RoomScript currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as RoomScript;
		currentRoom.transform.parent = this.transform;
		List<ADoorways> allAvailableDoorways = new List<ADoorways>(avDoorways);
		List<ADoorways> currentRoomDoorways = new List<ADoorways>();
		AddDoorwaysToList(currentRoom, ref currentRoomDoorways);
		AddDoorwaysToList(currentRoom, ref avDoorways);
		bool roomPlaced = false;

		foreach (ADoorways availableDoorway in allAvailableDoorways)
		{
			foreach (ADoorways currentDoorway in currentRoomDoorways)
			{
				PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

				if (CheckRoomOverlap(currentRoom))
				{
					continue;
				}

				roomPlaced = true;

				placedRooms.Add(currentRoom);

				currentDoorway.gameObject.SetActive(false);
				avDoorways.Remove(currentDoorway);

				availableDoorway.gameObject.SetActive(false);
				avDoorways.Remove(availableDoorway);
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
			ResetLevelGenerator();
		}
	}

	void PositionRoomAtDoorway(ref RoomScript room, ADoorways roomDoorway, ADoorways targetDoorway)
	{

		room.transform.position = Vector3.zero;
		room.transform.rotation = Quaternion.identity;

		Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
		Vector3 roomDoorwayEuler = roomDoorway.transform.eulerAngles;
		float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
		Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
		room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);
		Vector3 roomPositionOffset = roomDoorway.transform.position - room.transform.position;
		room.transform.position = targetDoorway.transform.position - roomPositionOffset;
	}

	bool CheckRoomOverlap(RoomScript room)
	{
		Bounds bounds = room.RoomBounds;
		bounds.Expand(-0.1f);

		Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, room.transform.rotation, roomLayerMask);
		if (colliders.Length > 0)
		{
			foreach (Collider c in colliders)
			{
				if (c.transform.parent.gameObject.Equals(room.gameObject))
				{
					continue;
				}
				else
				{
					return true;
				}
			}
		}

		return false;
	}

	void PlaceEndRoom()
	{
		endRoom = Instantiate(endRoomPrefab) as EndRoom;
		endRoom.transform.parent = this.transform;
		List<ADoorways> allAvailableDoorways = new List<ADoorways>(avDoorways);
		ADoorways doorway = endRoom.doorways[0];
		bool roomPlaced = false;

		foreach (ADoorways availableDoorway in allAvailableDoorways)
		{
			RoomScript room = (RoomScript)endRoom;
			PositionRoomAtDoorway(ref room, doorway, availableDoorway);

			if (CheckRoomOverlap(endRoom))
			{
				continue;
			}

			roomPlaced = true;
			doorway.gameObject.SetActive(false);
			avDoorways.Remove(doorway);
			availableDoorway.gameObject.SetActive(false);
			avDoorways.Remove(availableDoorway);
			break;
		}
		if (!roomPlaced)
		{
			ResetLevelGenerator();
		}
	}

	void ResetLevelGenerator()
	{
		StopCoroutine("GenerateLevel");

		if (startRoom)
		{
			Destroy(startRoom.gameObject);
		}

		if (endRoom)
		{
			Destroy(endRoom.gameObject);
		}

		foreach (RoomScript room in placedRooms)
		{
			Destroy(room.gameObject);
		}

		placedRooms.Clear();
		avDoorways.Clear();

		StartCoroutine("GenerateLevel");
	}
}
