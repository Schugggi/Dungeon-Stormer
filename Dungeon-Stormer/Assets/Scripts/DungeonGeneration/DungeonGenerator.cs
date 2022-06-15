using System;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private GameObject _hallwayScript;
    private GameObject[] _spawnedRooms;
    private GameObject[] _spawnPoints;
    private bool _roomsConnected = false;

    private void Start()
    {
        _hallwayScript = gameObject.transform.GetChild(1).gameObject;
        Invoke("SetSpawnPoints", 1f);
        Invoke("SetSpawnedRooms", 1f);
    }

    private void Update()
    {
        if (_roomsConnected is false)
        {
            Invoke("SpawnDungeon", 1.5f);
            _roomsConnected = true;
        }
    }

    private void SetSpawnPoints()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    private void SetSpawnedRooms()
    {
        _spawnedRooms = GameObject.FindGameObjectsWithTag("Room");
    }

    private void SpawnDungeon()
    {
        _hallwayScript.GetComponent<HallwayScript>().SetSpawnedRooms(_spawnedRooms);
        _hallwayScript.GetComponent<HallwayScript>().SetSpawnPoints(_spawnPoints);
        
        
    }
}
