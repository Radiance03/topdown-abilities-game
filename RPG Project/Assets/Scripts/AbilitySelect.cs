using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class AbilitySelect : MonoBehaviour
{

    private bool test = false;
    private bool[] isFrameTaken;
    public GameObject MarkChosenFrame;

    [SerializeField]
    private GameObject[] AbilitySloth;

    [SerializeField]
    private GameObject[] AbilitiesIcon;


    private Vector3[] AbilitiesIconPos;

    private void Update()
    {
        ClickTarget();
    }
    private void Start()
    {
        AbilitiesIconPos = new Vector3[AbilitiesIcon.Length];
        for(int i = 0; i < AbilitiesIcon.Length; i++)
        {
            AbilitiesIconPos[i] = AbilitiesIcon[i].transform.position;

        }

    }

    private void ArrangingIcons(int num,RaycastHit2D hit)
    {

    

   

            if (AbilitySloth[num].transform.position == hit.transform.position) //if its true it means the icon is on a sloth
            {
                for (int i = 0; i < AbilitiesIcon.Length; i++) //Finding the correct icon and getting it back to its place
                {
                    if (AbilitiesIcon[i].name == hit.transform.name)
                    {
                        hit.transform.position = AbilitiesIconPos[i];

                        return;


                    }

                }
            }


            for (int j = 0; j < AbilitiesIcon.Length; j++) //will make sure the chosen sloth doesnt have any icon
            {
                if (AbilitiesIcon[j].transform.position == AbilitySloth[num].transform.position)
                {
                    test = true; //already an ability is in place
                }
            }
            if (!test) //only if no icon ther is it will do this
            {
                hit.transform.position = AbilitySloth[num].transform.position;

            }



        
    }
    private void ClickTarget()
    {

        if (Input.GetMouseButtonDown(0))
        {
            test = false; //reset icon every clicks
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity); //send a raycast on the mouselick

            if (hit.collider != null) //if hit something with tag "enemy" set player.MyTarget to the hitbox of said enemy
            {
       

                if (hit.collider.tag == "AbilitySelectIcon")
                {
                    ArrangingIcons(1, hit);
                }



            

                if (hit.collider.tag == "ShieldSelectIcon")
                {
                    ArrangingIcons(0, hit);






                }

                if (hit.collider.tag == "SorcerySelectIcon")
                {
                    ArrangingIcons(2, hit);






                }

            }
          


        }

    }

    public void InitializePlayerPrefs()
    {
        
        for(int i = 0; i < AbilitySloth.Length; i++)
        {
            for(int j = 0; j < AbilitiesIcon.Length ; j++)
            {

                if (AbilitySloth[i].transform.position == AbilitiesIcon[j].transform.position)
                {
                    PlayerPrefs.SetInt("AbilitySloth" + i.ToString(), j);
                    break;
                }
            }

        }
        

        SceneManager.LoadScene("Map");
    }
}
