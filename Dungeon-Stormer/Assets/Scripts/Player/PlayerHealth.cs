using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int xp;                   //Player Experience and Levels
    public int xpNeeded = 100;
    private int level;

	public int health = 100;
	private int fullHealth = 100;

	//public GameObject deathEffect;
    public HealthBar healthBar;
	public Slider xpBar;
	public PlayerAttack playerDamage;
	public XpBar xpNumbers;
	//[SerializeField] UI_Inventory uI_Inventory;
	//private Inventory inventory;
	
    private void Start() {
		health = fullHealth;
        healthBar.SetMaxHealth(fullHealth);
		LoadGame();
		//inventory = new Inventory();
		//uI_Inventory.SetInventory(inventory);

		/*ItemWorld.SpawnItemWorld(new Vector3(45, 1), new Items{itemType = Items.ItemType.Armor, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(42, 1), new Items{itemType = Items.ItemType.Sword, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(49, 1), new Items{itemType = Items.ItemType.Healthpotion, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(49, 1), new Items{itemType = Items.ItemType.Healthpotion, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(49, 1), new Items{itemType = Items.ItemType.Healthpotion, amount = 1});
		ItemWorld.SpawnItemWorld(new Vector3(49, 1), new Items{itemType = Items.ItemType.Healthpotion, amount = 1});*/
    }
	/*private void OnTriggerEnter2D(Collider2D other) {
		ItemWorld itemWorld = other.GetComponent<ItemWorld>();
		if(itemWorld != null){
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
		}
	}*/

    public void TakeDamage(int damage)
	{
		health -= damage;
        healthBar.SetHealth(health - damage);

		StartCoroutine(DamageAnimation());

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}
    public void addExperience(int addXp)
    {
        xp += addXp;
		xpNumbers.showXpNumbers(addXp);
		//SaveGame();

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
		fullHealth += 15;
		health = fullHealth;
		healthBar.SetMaxHealth(fullHealth);
		xpBar.maxValue = xpNeeded;
		playerDamage.playerDamage += 5;
	}

    public void SaveGame()
    {
		PlayerPrefs.SetInt("Player Health", fullHealth);
		PlayerPrefs.SetInt("Player Level", level);
		PlayerPrefs.SetInt("Player XP", xp);
		PlayerPrefs.Save();

		Debug.Log("Saved Game!");
	}

	public void LoadGame()
	{
		if (PlayerPrefs.HasKey("Player XP"))
		{
			fullHealth = PlayerPrefs.GetInt("Player Health");
			level = PlayerPrefs.GetInt("Player Level");
			xp = PlayerPrefs.GetInt("Player XP");

			Debug.Log("Game data loaded!");
		}
		else
			Debug.Log("There is no save data!");
	}

	public void ResetGame()
    {
		PlayerPrefs.SetInt("Player Health", 100);
		PlayerPrefs.SetInt("Player Level", 0);
		PlayerPrefs.SetInt("Player XP", 0);
		PlayerPrefs.Save();

		Debug.Log("Reset Game!");
	}
}
