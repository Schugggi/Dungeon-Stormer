using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    [Header("References: ")]
    public Slider Slider;
    public  XpBarScript playerXp;
    [Space]
    
    private Text xpNumbers;
    void Start()
    {
        xpNumbers.canvasRenderer.SetAlpha(0);

        Slider.value = playerXp.xp;
        Slider.maxValue = playerXp.xpNeeded;
    }

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
