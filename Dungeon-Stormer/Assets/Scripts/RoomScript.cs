using UnityEngine;


public class RoomScript : MonoBehaviour
{
    private bool canSpawn = true;
    private Vector3 position;


    private void Start()
    {
        position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            canSpawn = false;
        }
    }

    public bool getCanSpawn()
    {
        return canSpawn;
    }
}
