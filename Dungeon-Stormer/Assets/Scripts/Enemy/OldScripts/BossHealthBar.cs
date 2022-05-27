using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

	public BossHealth bossHealth;
	public Slider slider;

	void Start()
	{
		if(slider != null)
        {
			slider.maxValue = bossHealth.health;
        }
	}


	public void FIxedUpdate()
    {
		slider.value = bossHealth.currentHealth;
    }
}