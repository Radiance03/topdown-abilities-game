using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{

    [SerializeField]
    protected float DamageDeal;
    [SerializeField]
    protected bool hasProjectile;

    private Transform target;
    public Transform Target
    {
        get
        {
            return target;
           
        }
        set
        {
            target = value;
        }
    }

    protected IEnumerator attack()
    {
        base.Update(); //Layer handling in Character

        isAttacking = true;
        myAnimator.SetBool("attack", true); //Attack animation set
        direction = Vector2.zero;

        if (!hasProjectile)//instantly take damage if the enemy is not a projectile type enemy
        {
            target.GetComponent<Character>().TakeDamage(DamageDeal);

        }
        yield return new WaitForSeconds(1); //wait a second..

  
        isAttacking = false; //attack is false and stop animation
        myAnimator.SetBool("attack", false);

    }


    protected virtual void FollowTarget()
    {
        if (Target != null) //if target exist go to target
        {
            direction = (Target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);

           
        }
        else
        {
            direction = Vector2.zero;
        }
    }


}
