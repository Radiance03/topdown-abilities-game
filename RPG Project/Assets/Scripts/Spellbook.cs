using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spellbook : MonoBehaviour
{
    //stores the spell data,responsible for updating the casting bar popup upon spellcast

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

    public float[] cooldown; //Cooldown for each spell, written here instead of in spell for less power usage



    private void Start()
    {
        cooldown = new float[spells.Length];
      
    }
    private void Update()
    {
        CalculateCooldown();
    }
    private Coroutine spellRoutine;

    public void CalculateCooldown()
    {
        for(int i = 0; i < cooldown.Length; i++)
        {
            spellCooldownFill[i].enabled = true;

            cooldown[i] += Time.deltaTime;

            spellCooldownFill[i].fillAmount = cooldown[i] / spells[i].MyCooldown;
            if (spells[i].MyCooldown < cooldown[i])
            {
                spellCooldownFill[i].enabled = false;   
                cooldown[i] = spells[i].MyCooldown;
              
            }

            textCooldown[i].text = cooldown[i].ToString("f1");
            

        }


    }
    public Spell CastSpell(int index) //all to do with the ui
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

        if (spells[index].MyStartAttackAnimation)
        {
            castingBar.fillAmount = 0;

            spellText.text = spells[index].MyName;
            castingBar.color = spells[index].MyBarColor;
            icon.sprite = spells[index].MyIcon;

            StartCoroutine(FadedBar());
            spellRoutine = StartCoroutine(Progress(index));
        }
     

        return spells[index];
    }

    private IEnumerator FadedBar()
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
        float timePassed = 0;

        float rate = 1.0f / spells[index].MyCastTime; 

        float progress = 0.0f;

        while(progress <= 1.0) //works for the amount of time of MyCastTime
        {
            castingBar.fillAmount = progress;

            progress += rate * Time.deltaTime; // progress gets 1/CastTime each second
            timePassed += Time.deltaTime;

            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");

            if(spells[index].MyCastTime - timePassed < 0)
            {
                castTime.text = "0.0";
                canvasGroup.alpha = 0;
            }


            yield return null;
        }
    }
    public void StopCasting()
    {
        if(spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
            canvasGroup.alpha = 0;

        }
    }
}

