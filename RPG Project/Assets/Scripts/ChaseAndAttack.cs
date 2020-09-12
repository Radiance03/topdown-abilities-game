using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAndAttack : Enemy
{


    [SerializeField]
    private float DistanceForAttack;

    [SerializeField]
    private GameObject Projectile;
    protected override void Update()
    {
        base.Update(); //Layer handling in Character

        //State Logic
        if (!isAttacking && !isDead) //Chase logic
        {
            FollowTarget();

        }
      
        if(Target != null) //Attck logic
        {
            if (Vector2.Distance(Target.position, transform.position) < DistanceForAttack && !isDead && !isAttacking) //if the distance is small enough to the player , enemy is alive and not attacking right now
            {
                StartCoroutine(attack()); //perform attack
                if (hasProjectile) //instantiate a projectile if the enemy has projectile
                {
                    var q = Instantiate(Projectile, transform.position, Quaternion.identity);
                    q.GetComponent<ArrowEnemy2>().Target = Target; //give the projectile the target

                }


            }
        }

    }

 

}
