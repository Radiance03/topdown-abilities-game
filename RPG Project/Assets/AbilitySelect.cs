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

    private void ClickTarget()
    {

        if (Input.GetMouseButtonDown(0))
        {
            test = false; //reset icon every clicks
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity); //send a raycast on the mouselick

            if (hit.collider != null) //if hit something with tag "enemy" set player.MyTarget to the hitbox of said enemy
            {
                if (hit.collider.tag == "AbilitySelectFrame")
                {


                    MarkChosenFrame.transform.position = hit.transform.position;

                    return;

                }

                if (hit.collider.tag == "AbilitySelectIcon")
                {
                    for (int j = 0; j < AbilitiesIcon.Length; j++)
                    {

                        if (AbilitySloth[j].transform.position == hit.transform.position) //if its true it means the icon is on a sloth
                        {
                            for (int i = 0; i < AbilitiesIcon.Length; i++)
                            {
                                if (AbilitiesIcon[i].name == hit.transform.name) //if its true we find i which is the origin point
                                {
                                    hit.transform.position = AbilitiesIconPos[i]; //we get it the fuck back to the origin point and make the frametaken false
                             
                                    return;


                                }

                            }
                        }

                    }


                    for (int i = 0; i < AbilitiesIcon.Length; i++)
                    {
                        if(MarkChosenFrame.transform.position == AbilitySloth[i].transform.position) //Figure out on which sloth MarkChosenFrame is
                        {
                            for(int j = 0; j < AbilitiesIcon.Length; j++) //will make sure the chosen sloth doesnt have any icon
                            {
                                if(AbilitiesIcon[j].transform.position == AbilitySloth[i].transform.position)
                                {
                                    test = true;
                                }
                                

                            

                            
                            }
                            if (!test) //only if no icon ther is it will do this
                            {
                                hit.transform.position = MarkChosenFrame.transform.position;

                            }

                        }
                    }
          
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
        

        SceneManager.LoadScene("SampleScene");
    }
}
