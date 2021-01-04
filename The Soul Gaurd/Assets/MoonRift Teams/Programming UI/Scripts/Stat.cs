using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stat : MonoBehaviour
{
    private Image contenet;

    private float currentFill;

    private float currentValue;

    [SerializeField]
    private TextMeshProUGUI statValue;

    [SerializeField]
    private float lerpSpeed;

    public float MyMaxValue { get; set; }

    public float MyCurrentValue 
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
         
            currentFill = currentValue / MyMaxValue;

            if (statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;
            }

        
        }
    }

    private void Start()
    {
        contenet = GetComponent<Image>();
    }

    public void Initialized(float currentValue, float maxValue)
    {
        if (contenet == null)
        {
            contenet = GetComponent<Image>();
        }

        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        contenet.fillAmount = MyCurrentValue / MyMaxValue;
    }

    private void Update()
    {
        if (currentFill != contenet.fillAmount)
        {
            contenet.fillAmount = Mathf.Lerp(contenet.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}