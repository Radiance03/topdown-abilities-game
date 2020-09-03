using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    public Transform MyTarget { get; set; }



    [SerializeField] //to be set in mana stat
    private Stat mana;
    private float InitMana = 50;

    [SerializeField] //blocks for LineOfSight()
    private Block[] blocks;

    private int exitIndex = 2; //for exitPoints
    private Spellbook spellBook;

    [SerializeField]
    private Transform[] exitPoints;  
    protected override void Update()
    {
        
        GetInput(); 
       
        base.Update();
    }
    protected override void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("AbilitySloth0"));
        Debug.Log(PlayerPrefs.GetInt("AbilitySloth1"));
        Debug.Log(PlayerPrefs.GetInt("AbilitySloth2"));



        //First Init spellbook and mana, then to the normal start
        spellBook = GetComponent<Spellbook>();
        mana.Initialize(InitMana, InitMana);

        base.Start();
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

        Transform currentTarget = MyTarget; //so changing myTarget wouldnt effect the Attack()
        Spell newSpell = spellBook.CastSpell(spellIndex); //newSpell stores the chosen spell based on spellIndex
        if(!isAttacking && inLineOfSight()) //conditions for attacking
        {
            isAttacking = true; //for attack layer to be activated

            if (newSpell.MyStartAttackAnimation)
            {
                myAnimator.SetBool("attack", isAttacking); //set attack to true
                yield return new WaitForSeconds(newSpell.MyCastTime); //do animation for the time of the spell

            }


            if (currentTarget != null && inLineOfSight())
            {
                
                SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>(); //instantiate the spell in the exitpoint and get the spellScript
                s.Initilize(currentTarget, newSpell.MyDamage);
            }
            StopAttack();


        }


    }
    public void CastSpell(int SpellIndex) //The first action after pressing the ability button
    {
        Block(); //Set up the block boundaries for LineOfSight()

        if (spellBook.cooldown[SpellIndex] == spellBook.spells[SpellIndex].MyCooldown && !isAttacking && inLineOfSight())
        {
            spellBook.cooldown[SpellIndex] = 0;

            if (MyTarget != null && !isAttacking && inLineOfSight()) //conditions
            {
                attackRoutine = StartCoroutine(Attack(SpellIndex));

            }

        }
       

    }

    private bool inLineOfSight()
    {
        if(MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized; //the vector that points to the target
            Debug.DrawRay(transform.position, targetDirection, Color.red); //debug ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);// throw a raycast from our position to the direction of the raycast with a 
            if (hit.collider == null) //if no contact it means there is a direct line from target to player else its blocked my the 256 layer
            {
                return true;
            }
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
    public override void StopAttack()
    {
        spellBook.StopCasting();
        base.StopAttack(); 
    }
}
