using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.tag == "Player")
        {
            enemyScript.Target = collision.transform;
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemyScript.Target = null;
        }
    }


}
