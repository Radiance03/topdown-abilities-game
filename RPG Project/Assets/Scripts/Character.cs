using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour //Speed, Health, Animation
{
    [SerializeField] private float speed;

    protected Vector2 direction;  //changed in players  

    [SerializeField]
    private float InitHealth;

    [SerializeField]
    protected Stat health;

    private protected Animator myAnimator;
    private Rigidbody2D myRigidbody;

    private protected bool isAttacking = false;
    protected Coroutine attackRoutine;
    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    protected virtual void Start()
    {
        health.Initialize(InitHealth, InitHealth);

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        
        HandleLayers(); //Change animation layers based on the player statee
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move() //Moving using direction which is set in the player/enemy
    {
        myRigidbody.velocity = direction.normalized * speed;
     
    }
    public void HandleLayers()
    {
    

        if (IsMoving && !isAttacking)
        {
            ActivateLayers("WalkLayer"); //WalkLayer
            //Update the x and y values
            myAnimator.SetFloat("X", direction.x); 
            myAnimator.SetFloat("Y", direction.y);
            StopAttack(); //make sure no attack is currently executed
        }
        else if (isAttacking)
        {
            ActivateLayers("AttackLayer");
        }
        else 
        {
            ActivateLayers("IdleLayer"); 
        }

    }

    public virtual void StopAttack()
    {
        isAttacking = false; //for the layer, goes out of the attack animation

        if (attackRoutine != null) //if attacking, stop the attack
        {
            StopCoroutine(attackRoutine);
        }
        myAnimator.SetBool("attack", isAttacking); //set attack to false

    }
    public virtual void TakeDamage(float damage) //Taking damage
    {
        
        health.myCurrentValue -= damage;
        if (health.myCurrentValue <= 0)
        {
            myAnimator.SetTrigger("die");
                
        }
    }
    public void ActivateLayers(string layerName) //Set the layer
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0); //shut down all layers
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1); //set the only chosen layer to activate

    }



}
