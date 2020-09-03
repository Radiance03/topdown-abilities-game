using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue;
    private float currentFill;

    public float MyMaxValue { get; set; }

    public float myCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if(value > MyMaxValue)
            {
                currentValue = MyMaxValue;

            }else if(value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
            currentFill = currentValue / MyMaxValue;

            if(statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;

            }
        }
    }
    private float currentValue;

    private void Start()
    {
        content = GetComponent<Image>();
    }
    private void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * 5);
        }
      
    }
    public void Initialize(float currentValue,float maxValue)
    {
        MyMaxValue = maxValue;
        myCurrentValue = currentValue;
    }
}
