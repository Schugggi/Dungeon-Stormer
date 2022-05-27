using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerDamage;
    public PlayerHealth playerXp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemyHealth = collision.GetComponent<BossHealth>();

        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth.TakeDamage(playerDamage);

            if (enemyHealth.currentHealth <= 0)
            {
                playerXp.addExperience(enemyHealth.XpAward);    
            }
        }
    }
}   
