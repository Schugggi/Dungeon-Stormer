using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    [Header("Angaben für Räume")]
    [Header("")]
    
    [Tooltip("Array für Räume die spawnen können")]
    public GameObject[] rooms;

    [Header("Grösse auf X-Achse einstellen")]
    public int minXAxe;
    public int maxXAxe;
    
    [Header("Grösse auf Y-Achse einstellen")]
    public int minYAxe;
    public int maxYAxe;

    [Header("Abstand zwischen den Räumen einstellen")]
    public int radius;
    [Header("Anzahl Räume")]
    public int roomAmount;

    [Header("Anzahl Versuche um die Räume zu generieren")]
    public int maximumTries;

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
            --maximumTries;
            if (maximumTries <= 0)
            {
                Debug.Log("Es konnten nicht alle Räume gespawnt werden!");
                break;
            }
        }
    }
}
