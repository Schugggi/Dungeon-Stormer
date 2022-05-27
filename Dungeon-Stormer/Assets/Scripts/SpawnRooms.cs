using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    public GameObject[] rooms;
    private List<GameObject> _spawnedRooms;

    public int minXAxe;
    public int maxXAxe;

    public int minYAxe;
    public int maxYAxe;

    public int radius;
    public int roomAmount;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DoSpawnRooms", 0.5f);
    }

    public void DoSpawnRooms()
    {
        for (int i = 0; i < roomAmount;)
        {
            Vector2 pos = new Vector2(Random.Range(minXAxe,
                maxXAxe), Random.Range(minYAxe, maxYAxe));
            if (Physics2D.OverlapCircleAll(pos, radius).Length < 1)
            {
                Instantiate(rooms[Random.RandomRange(0, rooms.Length)],
                     pos, Quaternion.identity);
                i++;
            }
        }
    }

    public List<GameObject> GetSpawnedRooms()
    {
        return _spawnedRooms;
    }

    private void AddSpawnedRoom(GameObject room)
    {
        _spawnedRooms.Add(room);
    }
}
