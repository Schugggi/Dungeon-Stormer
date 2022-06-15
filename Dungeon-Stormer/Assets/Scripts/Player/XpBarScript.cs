using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBarScript : MonoBehaviour
{
    [Header("Character statistics: ")]
    public int xp; 
    public int xpNeeded;
    private int level;
    [Space]
    [Header("References: ")]
    public Slider xpBar;
    public XpBar xpNumbers;
    public PlayerHealthScript phs;
    public HealthBar healthBar;
    private void Start() {
        
    }
    private void Update() {
        
    }
    public void addExperience(int addXp)
    {
        xp += addXp;
		xpNumbers.showXpNumbers(addXp);
        while(xp >= xpNeeded)
        {
			xp -= xpNeeded;
			addLevel();
			
        }
    }
    public void addLevel()
    {
		level++;
		xpNeeded += 20;
		phs.maxHealth += 15;
		phs.health = phs.maxHealth;
		healthBar.SetMaxHealth(phs.maxHealth);
		xpBar.maxValue = xpNeeded;
		playerDamage.playerDamage += 5;
	}
}
