using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [Header("Enemy statistics:")]
    public float speed;
    [Tooltip("Defines the range that the enemy can see")]
    public float distanceToChase;
    [Space]
    //value used to make math for the distance :)
    private float distance;
    [Header("References:")]
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if(distance < distanceToChase){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
}
