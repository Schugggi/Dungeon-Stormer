using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

	public int health = 500;
	public int currentHealth;
	public int XpAward;
	public BossHealthBar bossHealthBar;
	public EnemyHealthbar enemyHealthbar;
	public Slider slider;

	public GameObject deathEffect;


	public bool isInvulnerable = false;

    public void Start()
    {
		slider.maxValue = health;
		currentHealth = health;
    }

    private void Update()
    {
		slider.value = currentHealth;
	}

    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;
		
		currentHealth -= damage; 
		try {
			enemyHealthbar.showDamageNumbers(damage);
        }
        catch
        {

        }
        
		
		

		if (currentHealth <= 0)
		{
			Destroy(slider);
			Die();
		}
	}

	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
