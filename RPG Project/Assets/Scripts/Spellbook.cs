using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spellbook : MonoBehaviour
{
    //Stores the Spells and their data, will also update the casting bar

    public GameObject GameManager;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text spellText;

    [SerializeField]
    private Image[] spellCooldownFill;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private CanvasGroup canvasGroup;
    
    [SerializeField]
    public Spell[] spells;

    [SerializeField]
    public Text[] textCooldown;

    public float[] cooldown; //Cooldown for each spell, Each row corresponds to a new spell



    private void Start()
    {
        cooldown = new float[spells.Length]; 
      
    }
    private void Update()
    {
        CalculateCooldown(); 
    }
    private Coroutine spellRoutine;

    public void CalculateCooldown() //Increase each cooldown value, Update the Fill and text for castingbar 
    {
        for(int i = 0; i < spellCooldownFill.Length; i++) //Go through every cooldown
        {
            spellCooldownFill[i].enabled = true; //Enable the chosen fill
            spellCooldownFill[i].fillAmount = cooldown[i] / spells[i].MyCooldown; //Update the fill Amount
            cooldown[i] += Time.deltaTime; //Increase the cooldown time

            if (spells[i].MyCooldown < cooldown[i]) //if the the cooldown time is met
            {
                spellCooldownFill[i].enabled = false;   //Distable fill
                cooldown[i] = spells[i].MyCooldown; //Return cooldown value back to 0
              
            }

            textCooldown[i].text = cooldown[i].ToString("f1"); //Update text cooldown
            

        }


    }
    public Spell CastSpell(int index) //all to do with the ui Returns the chosen spell
    {
        switch (index)
        {
            case 0:
                index = GameManager.GetComponent<GameManager>().AbilityUsage[0];
                break;
            case 1:
                index = GameManager.GetComponent<GameManager>().AbilityUsage[1];
                break;
            case 2:
                index = GameManager.GetComponent<GameManager>().AbilityUsage[2];
                break;
          
        }
        /*
        if (spells[index].MyStartAttackAnimation)
        {
            castingBar.fillAmount = 0;

            spellText.text = spells[index].MyName;
            castingBar.color = spells[index].MyBarColor;
            icon.sprite = spells[index].MyIcon;

            StartCoroutine(FadedBar());
            spellRoutine = StartCoroutine(Progress(index));
        }
       */
     

        return spells[index];
    }

    private IEnumerator FadedBar() //every frame will up progress by the speed Rate and update the alpha
    {
        float rate = 1.0f /0.15f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = progress;
            progress += rate * Time.deltaTime;
          
   
            yield return null;
        }
    }
    private IEnumerator Progress(int index)
    {
        float timePassed = 0; //help display text and check if progress reached the duration of mycasttime

        float rate = 1.0f / spells[index].MyCastTime; 

        float progress = 0.0f;

        while(progress <= 1.0) //works for the amount of time of MyCastTime
        {
            castingBar.fillAmount = progress; //Update CastingBar fill

            progress += rate * Time.deltaTime; // progress gets 1/CastTime each second
            timePassed += Time.deltaTime;

            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if(timePassed > spells[index].MyCastTime)
            {
                castTime.text = "0.0";
                canvasGroup.alpha = 0;
            }


            yield return null;
        }
    }
    public void StopCasting()
    {
        if(spellRoutine != null) // set alpha to zero and stop coroutine and empty spellroutine
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
            canvasGroup.alpha = 0;

        }
    }
}

