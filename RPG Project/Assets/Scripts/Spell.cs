using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Spell
{

    // info of a spell, the spell data is stored in spellBook as an array
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private float cooldown;

    private float cooldownCount = 0;




    [Header("Configurations")]

    [SerializeField]
    private bool startAttackAnimation;

    [SerializeField]
    private bool TargetIsRequired;

    // [SerializeField]
    //private bool AimUsingMouse;

    // [SerializeField]
    //private bool NoRequirements;
    [SerializeField]
    private bool InstantInstantiate;

    



    [SerializeField]
    private Color barColor;

    public string MyName { get => name; }
    public int MyDamage { get => damage; }
    public Sprite MyIcon { get => icon; }
    public float MySpeed { get => speed; }
    public float MyCastTime { get => castTime; }
    public GameObject MySpellPrefab { get => spellPrefab; }
    public Color MyBarColor { get => barColor; }
    public bool MyStartAttackAnimation { get => startAttackAnimation; }
    public float MyCooldown { get => cooldown; }
    public bool MyBasicSpell { get => TargetIsRequired; }
    // public bool MyAimByMouse { get => AimUsingMouse; }
    //  public bool MyNoRequirements { get => NoRequirements; }
    public bool MyInstantInstantiate { get => InstantInstantiate; }





}
