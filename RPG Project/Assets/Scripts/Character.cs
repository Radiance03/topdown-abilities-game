using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour //Speed, Health, Animation
{
    protected Vector2 direction;  //changed in players  
    [SerializeField] protected float speed;

  
    private protected bool isDead = false;
    private protected bool isAttacking = false; //set to true in attack() and to false in stopattack()
    protected Coroutine attackRoutine; //assigned in player so we can cancel the coroutine


    [SerializeField] private float InitHealth; //the starting hp
    [SerializeField] protected Stat health; //hp stat


    //rb and anim
    private protected Animator myAnimator;
    private Rigidbody2D myRigidbody;

    

    public bool IsMoving //player's movement
    {
        get
        {
            return direction.x != 0 || direction.y != 0; 
        }
    }

    protected virtual void Start()
    {
        health.Initialize(InitHealth, InitHealth); //HP stat init

        //rb and animator
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        
        HandleLayers();
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move() //Moving using direction which is set in the player/enemy
    {
        myRigidbody.velocity = direction.normalized * speed;
     
    }
    public void HandleLayers() //Set the layers based on inputs
    {


        if (IsMoving && !isAttacking && !isDead)
        {
            ActivateLayers("WalkLayer");
            myAnimator.SetFloat("X", direction.x);
            myAnimator.SetFloat("Y", direction.y);
            //StopAttack();
        }
        else if (isAttacking && !isDead)
        {
            Debug.Log("test");
            ActivateLayers("AttackLayer");
        }
        else if (isDead)
        {
            ActivateLayers("DeathLayer");
            direction = Vector2.zero;

        }
        else 
        {
            ActivateLayers("IdleLayer");
        }

    }

    public virtual void TakeDamage(float damage) //Taking damage
    {
        
        health.myCurrentValue -= damage;
        if (health.myCurrentValue <= 0)
        {
            isDead = true;
            
            myAnimator.SetTrigger("die");
                
        }
    }
    public void ActivateLayers(string layerName) //Set the layer of animation
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0); //shut down all layers
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1); //set the only chosen layer to activate

    }



}
