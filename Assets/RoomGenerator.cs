using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RoomGenerator : MonoBehaviour
{
    public List<GameObject> rooms;
    public Dictionary<Vector2, int> placedRooms = new Dictionary<Vector2, int>();
    public List<GameObject> placeholder;
    public int seed;
    public int numberOfRooms;
    int lastDirection;
    int currentRooms = 0;
    public int maxIterations = 12;
    List<Vector2> addedRooms = new List<Vector2>();
    public float differenceX, differenceZ;
    public Vector3 spawnPoint;

    [Header("Boxed Properties")]
    public int sizeX;
    public int sizeY;
    public int groundPercent;

    public void GenerateNewMap(int randSeed)
    {
        seed = randSeed;
        Generate();
        //spawnPoint = new Vector3(addedRooms[0].x, 0, addedRooms[0].y);
        RecountNavMesh();
    }

    void RecountNavMesh()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void Generate()
    {
        if (seed != 0)
        {
            Random.InitState(seed);
            //GenerateDictionary();
            GenerateBoxed();
            InstantiateRooms();
        }
        else
        {
            seed = Random.seed;
            //GenerateDictionary();
            GenerateBoxed();
            SpawnFix();
            //GeneratePlaceholderPositions();
            InstantiateRooms();
        }
    }

    private void SpawnFix()
    {
        placedRooms[Vector2.zero] = 0;
    }

    private void GeneratePlaceholderPositions()
    {
        Dictionary<Vector2, int> placedRoomsTemp = placedRooms.ToDictionary(entry => entry.Key, entry => entry.Value);
        foreach (Vector2 v2 in placedRoomsTemp.Keys)
        {
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x, v2.y + differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x, v2.y + differenceZ), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x, v2.y - differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x, v2.y - differenceZ), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x + differenceX, v2.y)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x + differenceX, v2.y), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x - differenceX, v2.y)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x - differenceX, v2.y), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x + differenceX, v2.y + differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x + differenceX, v2.y + differenceZ), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x - differenceX, v2.y + differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x - differenceX, v2.y + differenceZ), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x + differenceX, v2.y - differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x + differenceX, v2.y - differenceZ), -1);
                }
                catch { }
            }
            if (!placedRoomsTemp.ContainsKey(new Vector2(v2.x - differenceX, v2.y - differenceZ)))
            {
                try
                {
                    placedRooms.Add(new Vector2(v2.x - differenceX, v2.y - differenceZ), -1);
                }
                catch { }
            }
        }
    }


    private void InstantiateRooms()
    {
        foreach (Vector2 v in placedRooms.Keys)
        {
            if (placedRooms[v] >= 0)
            {
                Instantiate(rooms[placedRooms[v]], new Vector3(v.x * differenceX, 0, v.y * differenceZ), rooms[placedRooms[v]].transform.rotation);
            }
            else
            {
                int placeholderRandom = Random.Range(0, placeholder.Count);
                Instantiate(placeholder[placeholderRandom], new Vector3(v.x * differenceX, 0, v.y * differenceZ), placeholder[placeholderRandom].transform.rotation);
            }
        }
    }

    private void GenerateBoxed()
    {
        bool inverseX = Random.Range(0, 2) == 0 ? false : true;
        bool inverseY = Random.Range(0, 2) == 0 ? false : true;
        int lastRandomed = 0;
        int i = 0, j = 0;
        int maxI, maxJ;
        if (inverseX)
        {
            i = -sizeX + 1;
            maxI = 1;
        }
        else
        {
            i = 0;
            maxI = sizeX;
        }

        if (inverseY)
        {
            j = -sizeY + 1;
            maxJ = 1;
        }
        else
        {
            j = 0;
            maxJ = sizeY;
        }

        for (int x = i; x < maxI; x++)
        {
            for (int y = j; y < maxJ; y++)
            {
                //if (randomedNumber > groundPercent)
                //{
                int randomedRoom = Random.Range(0, rooms.Count);
                while (randomedRoom == lastRandomed)
                {
                    randomedRoom = Random.Range(0, rooms.Count);
                }
                placedRooms.Add(new Vector2(x, y), randomedRoom);
                lastRandomed = randomedRoom;
                //}
                //else
                //{
                //    int randomedRoom = Random.Range(1, rooms.Count);
                //    placedRooms.Add(new Vector2(i, j), -randomedRoom);
                //}
            }
        }
        ContainMap();
    }

    private void ContainMap()
    {
        Dictionary<Vector2, int> tempRooms = new Dictionary<Vector2, int>(placedRooms);
        foreach (var room in tempRooms.Keys)
        {
            tempRooms = new Dictionary<Vector2, int>(placedRooms);
            if (!tempRooms.ContainsKey(new Vector2(room.x + 1, room.y + 1)))
            {
                placedRooms.Add(new Vector2(room.x + 1, room.y + 1), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x, room.y + 1)))
            {
                placedRooms.Add(new Vector2(room.x, room.y + 1), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x + 1, room.y)))
            {
                placedRooms.Add(new Vector2(room.x + 1, room.y), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x - 1, room.y + 1)))
            {
                placedRooms.Add(new Vector2(room.x - 1, room.y + 1), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x + 1, room.y - 1)))
            {
                placedRooms.Add(new Vector2(room.x + 1, room.y - 1), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x, room.y - 1)))
            {
                placedRooms.Add(new Vector2(room.x, room.y - 1), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x - 1, room.y)))
            {
                placedRooms.Add(new Vector2(room.x - 1, room.y), -1);
            }
            if (!tempRooms.ContainsKey(new Vector2(room.x - 1, room.y - 1)))
            {
                placedRooms.Add(new Vector2(room.x - 1, room.y - 1), -1);
            }
        }
    }

    private void GenerateDictionary()
    {
        int roomNumber = 0;
        while (currentRooms < numberOfRooms)
        {
            //maxIterations = 0;
            int iterations = 0;
            roomNumber = Random.Range(0, rooms.Count);
            int direction = Random.Range(0, 4);
            if (addedRooms.Count <= 0)
            {
                addedRooms.Add(Vector2.zero);
                placedRooms.Add(Vector2.zero, 0);
                currentRooms++;
            }
            while (!GenerateNewRoom(direction, roomNumber, addedRooms[addedRooms.Count - 1]) && iterations < maxIterations)
            {
                roomNumber = Random.Range(0, rooms.Count);
                direction = Random.Range(0, 4);
                iterations++;
            }
            if (iterations >= maxIterations)
            {
                placedRooms.Remove(addedRooms[addedRooms.Count - 1]);
                currentRooms--;
                addedRooms.Remove(addedRooms[addedRooms.Count - 1]);
            }
        }
    }

    bool GenerateNewRoom(int direction, int roomNumber, Vector2 lastRoom)
    {
        Debug.Log(direction + " : " + roomNumber + " : " + lastRoom);
        try
        {
            if (direction == 0)
            {
                placedRooms.Add(new Vector2(lastRoom.x, lastRoom.y + differenceZ), roomNumber);
                addedRooms.Add(new Vector2(lastRoom.x, lastRoom.y + differenceZ));
            }
            else if (direction == 1)
            {
                placedRooms.Add(new Vector2(lastRoom.x + differenceX, lastRoom.y), roomNumber);
                addedRooms.Add(new Vector2(lastRoom.x + differenceX, lastRoom.y));
            }
            else if (direction == 2)
            {
                placedRooms.Add(new Vector2(lastRoom.x, lastRoom.y - differenceZ), roomNumber);
                addedRooms.Add(new Vector2(lastRoom.x, lastRoom.y - differenceZ));
            }
            else
            {
                placedRooms.Add(new Vector2(lastRoom.x - differenceX, lastRoom.y), roomNumber);
                addedRooms.Add(new Vector2(lastRoom.x - differenceX, lastRoom.y));
            }
            currentRooms++;
            return true;
        }
        catch
        {
            return false;
        }
    }
}
