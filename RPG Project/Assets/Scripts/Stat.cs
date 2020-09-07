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

    public float myCurrentValue //The value we modify (health.myCurrentValue -= damage)
    {
        get
        {
            return currentValue;
        }
        set //make sure the value is less or equal to max and min , update currentFill to the new currentValue because it was just modified
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

            if(statValue != null) //Update text 
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
        if(currentFill != content.fillAmount) // when myCurrentValue is changed currentFill will be modified, when it does we lerp fillamount until it reaches the new currenfill
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * 5);
        }
      
    }
    public void Initialize(float currentValue,float maxValue) //Called to init the stat, mana.Init(x,y)
    {
        MyMaxValue = maxValue;
        myCurrentValue = currentValue;
    }
}
