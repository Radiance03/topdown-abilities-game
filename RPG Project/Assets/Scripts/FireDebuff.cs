using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : SpellScript
{

    public GameObject fireIcon;
    GameObject fire;


    public override void Debuff(Collider2D collision)
    {
        if(collision != null)
        {
            fire = Instantiate(fireIcon, collision.transform.position, Quaternion.identity);
            fire.transform.SetParent(collision.transform);

            StartCoroutine(DelayedDamage(collision));
        }
  
    }

    private IEnumerator DelayedDamage(Collider2D collision)
    {

        for(int i = 0; i < 3; i++)
        { 
            yield return new WaitForSeconds(1);

            if(collision != null)
            {
                collision.GetComponentInParent<Enemy>().TakeDamage(1);

            }
            else
            {
                break;
            }
        }

        Destroy(fire);


    }
}
