using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    [SerializeField]
    private float speed;


    [SerializeField]
    private int damage;
    private Rigidbody2D myRigidBody;

    public Transform MyTarget { get; private set; }
    // Start is called  fgefore the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void Initilize(Transform target, int damage)
    {
        this.MyTarget = target;
        
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;
            myRigidBody.velocity = direction.normalized * speed; //go in the direction

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward); //look at the target

        }
       



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform == MyTarget)   //if collide with player set puff animation stop the velocity and null the target 
        {
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(damage);
            GetComponent<Animator>().SetTrigger("Impact");
            myRigidBody.velocity = new Vector2(0, 0);
            MyTarget = null;
        }
    }
}
