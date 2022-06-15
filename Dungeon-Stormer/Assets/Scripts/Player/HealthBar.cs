using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
	
	public Slider slider;
    private int health;  
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    
}
