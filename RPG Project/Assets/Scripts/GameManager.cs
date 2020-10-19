using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour    
{
    [SerializeField]
    private Button[] b;
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
        InitializeEnemies();
    }


  
    [SerializeField]
    private int NumberOfEnemies;
    [SerializeField]
    private GameObject []Enemies;
    [SerializeField]
    private Vector2[] EnemiesSpawn;

   
    private void InitializeEnemies()
    {
        for(int i = 0; i < NumberOfEnemies; i++)
        {
            var q = Instantiate(Enemies[i], EnemiesSpawn[i], Quaternion.identity);

        
        }
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
        ButtonKeyboardInteractable();
        ClickTarget();
        TargetCrossPlacement();
    }

    private void TargetCrossPlacement()
    {
        if (player.MyTarget != null)
        {
            TargetCross.SetActive(true);
            TargetCross.transform.position = player.MyTarget;

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

            player.MyTarget= new Vector2(0,0);
            player.CastSpell(1);





        }
       
    }
    public void ButtonKeyboardInteractable()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            b[0].onClick.Invoke();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            b[1].onClick.Invoke();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            b[2].onClick.Invoke();

        }

    }
}




