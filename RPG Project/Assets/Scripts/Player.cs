﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    public Vector2 MyTarget { get; set; }




    int[] temp = new int[3];
    [SerializeField] //to be set in mana stat
    private Stat mana;
    private float InitMana = 50;

    [SerializeField] //blocks for LineOfSight()
    private Block[] blocks; //Limit player vision used by InLineOfSight()

    private int exitIndex = 2; //for exitPoints which is where the projectile spawns based on the animation
    private Spellbook spellBook;

    [SerializeField]
    private Transform[] exitPoints; //Where the projectiles spawn  
    protected override void Update()
    {   
        GetInput(); //calculate the player input , Direction is to store movement, exitIndex is corresponding to the animation

        if (isAttacking)
        {
            if(MyTarget != null)
            {
                Vector2 AttackingFaceDirection = (MyTarget - (Vector2)transform.position).normalized;
                myAnimator.SetFloat("X", AttackingFaceDirection.x);
                myAnimator.SetFloat("Y", AttackingFaceDirection.y);
            }
         

        }


      

        base.Update(); //Preform the rest of update in Character after gaining input
    }

    
  
    protected override void Start()
    {

        spellBook = GetComponent<Spellbook>();
        mana.Initialize(InitMana, InitMana);//Initialize mana (Stat Script)

        base.Start(); //Preform the rest of start in character
    }

   
    private void GetInput() //get the Direction for movement and exitindex for where the ability should shoot from
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 3;
            direction += Vector2.up;
        }
            
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 1;
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 0;
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 2;
            direction += Vector2.right;
        }

    }
   

    private IEnumerator Attack(int spellIndex) //called in CastSpell()
    {
        yield return null;
        Spell newSpell = spellBook.CastSpell(spellIndex); //newSpell stores the chosen spell based on spellIndex
        float saveSpeed = speed; //Player Speed saved for casting animation slow down
        Vector2 currentTarget = MyTarget; //so changing myTarget wouldnt effect the Attack()



        // ----- ATTACK LOGIC ------
        if (!isAttacking) //conditions for attacking
        {
            isAttacking = true; //for attack layer to be activated

            PlayerLookAtTarget();
            if (newSpell.MyStartAttackAnimation) // ANIMATION CASE
            {
                speed = 2.5f; //Player Speed slowed
                myAnimator.SetBool("attack", isAttacking); //set attack to true
                if (newSpell.MyInstantInstantiate) //if for Instantiate Before cooldown
                {
                    Instantiate(newSpell.MySpellPrefab, transform.position, Quaternion.identity);

                }
                yield return new WaitForSeconds(newSpell.MyCastTime); //do animation for the time of the spell
                speed = saveSpeed; //speed back to normal after spell cast

            }




            AttackLogic(spellIndex, newSpell, currentTarget);
            
            StopAttack();
        }
    }

    private void AttackLogic(int spellIndex, Spell newSpell, Vector2 currentTarget)
    {
        if (newSpell.MyBasicSpell) //  BASIC SPELL ATTACK CASE
        {

        
                PlayerLookAtTarget();

                SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>(); //instantiate the spell in the exitpoint and get the spellScript
                s.mySpeed(spellBook.spells[spellIndex].MySpeed);
                s.InitilizeV3(Camera.main.ScreenToWorldPoint(Input.mousePosition), newSpell.MyDamage);
            
        }

        else //INSTANT DROP CASE
        {
            if(newSpell.MyName == "Teleport")
            {
                TelportationAbility();
            }

        }
    }

    private void TelportationAbility()
    {
        transform.position = new Vector2(0, 0);
    }



    public void CastSpell(int SpellIndex) //The first action after pressing the ability button
    {
        Spell newSpell = spellBook.CastSpell(SpellIndex);

        //Block(); //Set up the vision block for LineOfSight()

   
        
            if (spellBook.cooldown[SpellIndex] == spellBook.spells[SpellIndex].MyCooldown && !isAttacking)
            {
                spellBook.cooldown[SpellIndex] = 0;

                if (!isAttacking) //conditions
                {
                    attackRoutine = StartCoroutine(Attack(SpellIndex)); //main attack
                }

            }
        
    }
    private bool PlayerLookAtTarget() //Faces the player in the direction of MyTarget
    {
        for (int i = 0; i < blocks.Length; i++) //go through every block and test it
        {
            BlockWithArgument(i);
            if (inLineOfSight()) //the current block is the one that is facing the player
            {


                //for all cases
                if (i == 0)
                {
                    exitIndex = 0;

                    myAnimator.SetFloat("X", 0);
                    myAnimator.SetFloat("Y", -1);

                }
                if (i == 1)
                {
                    exitIndex = 1;

                    myAnimator.SetFloat("X", -1);
                    myAnimator.SetFloat("Y", 0);
                }
                if (i == 2)
                {
                    exitIndex = 2;

                    myAnimator.SetFloat("X", 1);
                    myAnimator.SetFloat("Y", 0);
                }
                if (i == 3)
                {
                    exitIndex = 3;

                    myAnimator.SetFloat("X", 0);
                    myAnimator.SetFloat("Y", 1);
                }


                return true;
            }


        }
        return false;
    }

    private bool inLineOfSight()
    {
      
            Vector3 targetDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized; //the vector that points to the target
            Debug.DrawRay(transform.position, targetDirection, Color.red); //debug ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), 256);// throw a raycast from our position to the direction of the raycast with a 
            if (hit.collider == null) //if no contact it means there is a direct line from target to player else its blocked my the 256 layer
            {
                return true;
            }
        

        return false;

    }
    private void Block()
    {
        foreach(Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
    private void BlockWithArgument(int index)
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[index].Activate();
    }
    public void StopAttack()
    {
        spellBook.StopCasting();
        isAttacking = false; //for the layer

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine); //stop the attack roroutine
        }
        myAnimator.SetBool("attack", isAttacking); //set attack to false

    }



    

}
