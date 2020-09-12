using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{

    [SerializeField]
    private GameObject Projectile;
 
    void Update()
    {
        //State logic
        if (!isAttacking && !isDead) //Chase logic
        {
            FollowTarget();

        }

        if (Target != null && !isAttacking) //Attck logic
        {
            if (Vector2.Distance(Target.position, transform.position) < 2 && !isDead)
            {
                StartCoroutine(attack());
                Instantiate(Projectile, transform.position, Quaternion.identity);

            }
        }

        base.Update(); //Layer handling in Character
    }
}
