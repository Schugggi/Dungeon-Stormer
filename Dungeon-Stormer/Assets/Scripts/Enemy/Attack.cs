using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;
    public Collider2D range;
    public PlayerHealthScript playerHealth;
    //public GameObject player;

    public void AttackPlayer(){
        playerHealth.TakeDamage(attackDamage);
    }
    void OnCollisionEnter2D(Collision2D col){
        if(range.gameObject.tag == "Player"){
            AttackPlayer();
        }
    }
}
