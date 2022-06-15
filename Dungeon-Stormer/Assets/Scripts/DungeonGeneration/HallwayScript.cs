using UnityEngine;

public class HallwayScript : MonoBehaviour
{
    private GameObject[] spawnedRooms;
    private GameObject[] spawnPoints;
    private float timer = 1f;
    private bool logged = false;
    void Start()
    {
        
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && logged is false)
        {
            
            logged = true;
        }
    }
    
    

    public void SetSpawnedRooms(GameObject[] rooms)
    {
        spawnedRooms = rooms;
    }

    public void SetSpawnPoints(GameObject[] points)
    {
        spawnPoints = points;
    }
}
