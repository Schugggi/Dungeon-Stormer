using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] public Slider Slider;
    [SerializeField] public Text damageNumbers;
    [SerializeField] public PlayerAttack AttackDamage;
    [SerializeField] public BossHealth Enemy;
    [SerializeField] public Vector3 Offset;
    [SerializeField] public Vector3 Offset2;

    void Start()
    {
        damageNumbers.canvasRenderer.SetAlpha(0);

        Slider.gameObject.active = false;
        Slider.value = Enemy.health;
        Slider.maxValue = Enemy.health;
    }

    void Update()
    {
        if (Enemy.health > Enemy.currentHealth)
        {
            Slider.gameObject.active = true;
        }

        damageNumbers.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset2);
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        Slider.value = Enemy.currentHealth;
    }

    public void showDamageNumbers(int damage)
    {
        damageNumbers.text = "-" + damage;
        damageNumbers.canvasRenderer.SetAlpha(1f);
        damageNumbers.CrossFadeAlpha(0f, 0.5f, true);
    }
}
