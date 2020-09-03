using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour    
{
    [SerializeField]
    private GameObject[] Abilities;


    public int[] AbilityUsage;

    [SerializeField]
    private Image[] existingAbilities;

    public GameObject TargetCross; 
    [SerializeField]
    private Player player;

    private NPC currentTarget;

    private void Start()
    {
       InitializeAbilityBoard();
    }

    private void InitializeAbilityBoard()
    {
        AbilityUsage = new int[Abilities.Length];
        for(int i = 0; i < Abilities.Length; i++)
        {
            Abilities[i].GetComponent<Image>().sprite = existingAbilities[(PlayerPrefs.GetInt("AbilitySloth" + i.ToString()))].sprite;
            AbilityUsage[i] = PlayerPrefs.GetInt("AbilitySloth" + i.ToString());
        }
    }
    void Update()
    {
        ClickTarget();
        TargetCrossPlacement();
    }

    private void TargetCrossPlacement()
    {
        if (player.MyTarget != null)
        {
            TargetCross.SetActive(true);
            TargetCross.transform.position = player.MyTarget.transform.position;

        }
        else
        {
            TargetCross.SetActive(false);
        }
    }

    private void ClickTarget()
    {
       

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,512); //send a raycast on the mouselick

            if(hit.collider != null) //if hit something with tag "enemy" set player.MyTarget to the hitbox of said enemy
            {
                if(hit.collider.tag == "Enemy")
                {

                    player.MyTarget = hit.transform.GetChild(0);

                }
            }
            else
            {
                player.MyTarget = null;
            }
        }
       
    }
}
