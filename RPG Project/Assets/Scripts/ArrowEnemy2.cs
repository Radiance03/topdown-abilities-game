using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy2 : MonoBehaviour
{


    Vector2 direction;
    float angle;
    Rigidbody2D myRigidbody;
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


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); //RB
        direction = Target.position - transform.position; //direction for the arrow
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //angle
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward); //look at the target


    }
    void Update()
    {

        myRigidbody.velocity = direction.normalized * 3; //go in the direction

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") //if hit player deal damage and destroy
        {
            collision.GetComponent<Character>().TakeDamage(5);
            Destroy(gameObject);

        }
    }
}
