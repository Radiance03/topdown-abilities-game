using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    public BoxCollider2D boxCol;
    private float destroyTimer = 8;
    private float speed = 5;
    public void mySpeed(float speedValue)
    {
        speed = speedValue;
    }

    private void Update()
    {
        destroyTimer -= Time.deltaTime;
        if(destroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }


    [SerializeField]
    private int damage;
    private Rigidbody2D myRigidBody;

    public Transform MyTarget { get; set; }
    public Vector3 MyTargetVector2 { get; set; }
    // Start is called  fgefore the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void Initilize(Transform target, int damage)
    {
        this.MyTarget = target;
        
    }
    bool Vector3Initialized;
    public void InitilizeV3(Vector2 target, int damage)
    {
        this.MyTargetVector2 = target;
        Vector3Initialized = true;

    }

    public virtual void Debuff(Collider2D Collision)
    {

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
        else
        {
            if (!Vector3Initialized)
            {
                Destroy(gameObject);

            }
        }
        
        if(Vector3Initialized)
        {


            Vector2 direction = MyTargetVector2 - transform.position;
            myRigidBody.velocity = direction.normalized * speed; //go in the direction

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward); //look at the target

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox"  && collision != null && (collision.transform == MyTarget || Vector3Initialized))   //if collide with player set puff animation stop the velocity and null the target 
        {
      
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(damage); //Take Initial Damage
            GetComponent<Animator>().SetTrigger("Impact"); //Puff animation
            myRigidBody.velocity = new Vector2(0, 0); //Stop moving
            boxCol.size = new Vector2(0, 0);
            MyTarget = null; //so it wont move
    
            Debuff(collision);
           

        }
    }
}
