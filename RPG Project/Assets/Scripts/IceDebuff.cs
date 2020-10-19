using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : SpellScript
{
    public override void Debuff(Collider2D collision)
    {
        if(collision != null)
        {
            StartCoroutine(SlowEffect(collision));
        }

    }

    private IEnumerator SlowEffect(Collider2D col)
    {
        if (col != null)
        {
           

            float var = col.GetComponentInParent<Character>().MyOGSpeed;
            col.GetComponentInParent<Enemy>().EnemySpeed = 0.2f;
            col.GetComponentInParent<SpriteRenderer>().color = new Color(0.38f, 90f, 1f); //update color;

            col.GetComponentInParent<Animator>().speed = 0.2f;
            col.GetComponentInParent<Enemy>().attackWaitForSeconds = 2;


            yield return new WaitForSeconds(3);

            col.GetComponentInParent<Enemy>().attackWaitForSeconds = 1;
            col.GetComponentInParent<Animator>().speed = 1
                ;
            col.GetComponentInParent<SpriteRenderer>().color = new Color(1f, 1f, 1f); //update color;
            col.GetComponentInParent<Enemy>().EnemySpeed = var;
            Destroy(gameObject);

        }

    }


    
       
    
}
