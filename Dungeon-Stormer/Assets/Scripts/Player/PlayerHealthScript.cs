using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealthScript : MonoBehaviour
{
    [Header("Character Statistics: ")]
    public int health;
    public int maxHealth;
    [Space]
    [Header("References: ")]
    public HealthBar healthBar;

    private void Start() {
        health = maxHealth;
        healthBar.SetMaxHealth(health);
    }
    public void TakeDamage(int damage){
        if(health <= 0){
            Die();
        }else{
            health -= damage;
            healthBar.SetMaxHealth(health);
        }
    }
    public void Die(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
