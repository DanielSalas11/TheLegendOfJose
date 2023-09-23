using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialBar : MonoBehaviour
{

    public Image fill;
    public TextMeshProUGUI amount;
    public int currentValue, maxValue;

    public void Add(int value)
    {
        currentValue += value;

        if (currentValue > maxValue)
        {
            currentValue = maxValue;
        }
        if (currentValue < 1)
        {
            currentValue = 0;
        }
        fill.fillAmount = Normalise();
        amount.text = $"{currentValue}/{maxValue}";
    }

    private float Normalise()
    {
        return (float)currentValue / maxValue;
    }
}
