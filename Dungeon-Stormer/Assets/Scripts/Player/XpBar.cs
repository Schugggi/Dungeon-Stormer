using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [SerializeField] public Slider Slider;
    [SerializeField] public PlayerHealth playerXp;
    [SerializeField] private Text xpNumbers;
    // Start is called before the first frame update
    void Start()
    {
        xpNumbers.canvasRenderer.SetAlpha(0);

        Slider.value = playerXp.xp;
        Slider.maxValue = playerXp.xpNeeded;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.value = playerXp.xp;
        Slider.maxValue = playerXp.xpNeeded;
    }

    public void showXpNumbers(int xp)
    {
        xpNumbers.text = "+" + xp;
        xpNumbers.canvasRenderer.SetAlpha(1f);
        xpNumbers.CrossFadeAlpha(0f, 2f, true);
    }
}
