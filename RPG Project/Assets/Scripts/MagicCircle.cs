using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{

    float timer = 1f;
    bool AllowContact;
    bool CallOnceInUpdate = true;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;


        if (timer > 0)
        {
            transform.position = Player.transform.position;

        }
        else
        {
            if (CallOnceInUpdate)
            {
                CallOnceInUpdate = false;

                MagicCircleDebuff();


            }


        }

    }
    public virtual void MagicCircleDebuff()
    {
        //debuff called in child script

    }
}

       




            
        
    
    



