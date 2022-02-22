//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// Controls Enemy to Player combat. Also controls Player to Enemy combat.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    private bool inRange = false;
    private bool hit = false;
    private bool isDead = false;
    private bool phit = false;
    private float timer = 0.0f;
    private JButler_PlayerMat mats = null;
    private Player_Values player = null;
    private Overlord ov = null;

    [Header("Object References")]
    [Tooltip("Needed for health values. Connected to the parent.")]
    [SerializeField] Player_Values pv = null;
    [Tooltip("Needed to shoot properly. Connected to the parent.")]
    [SerializeField] JButler_Shooting shooting = null;

    [Header("Different States")]
    [Tooltip("You can costumize this to represent the three states damage goes in. \n0) Default \n1) Selected \n2) Hit")]
    [SerializeField] private Material[] states = null;
    [Tooltip("How long between damage can be taken. Also how long 2) Hit will show before switching back to 0) Default.")]
    [SerializeField] private float flashDamage = 1.0f;

    [Header("Enemy Damage Types")]
    [Tooltip("If you touch or continue to touch an enemy, you will take damage.")]
    [SerializeField] private bool melee = false;
    [Tooltip("Will allow the enemy to shoot at the player.")]
    [SerializeField] private bool shooter = false;
    [Tooltip("Will allow the enemy to Explode!")]
    [SerializeField] private bool exploder = false;

    [Header("Damage Amounts")]
    [Tooltip("Used by: Patroller and Hazard")]
    [SerializeField] private int meleeDamage = 5;
    [Tooltip("Used by: Exploder")]
    [SerializeField] private int explosionDamage = 50;
    [Tooltip("Time to Kill is used for continuous damage.\nUsed by: Patroller and Hazard")]
    [SerializeField] private float ttk = 1.0f;


    SoundManager sndmngr = null;
    [SerializeField]
    Animation anim = null;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player_EGO").GetComponent<Player_Values>();
        mats = FindObjectOfType<JButler_PlayerMat>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        ov = FindObjectOfType<Overlord>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyHighlighted();

        //if (Input.GetKeyDown(KeyCode.Space) && shooter)
        //{
        //    shooting.ShootProjectile();
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && exploder)
        //{
        //    GetComponentInParent<JButler_Agent>().Detonate();
        //}
    }

    //////////////////////////////////////////////////
    // Player > Enemy Methods
    //////////////////////////////////////////////////

    public bool InMeleeRange(bool inMeleeRange)
    {
        if (inMeleeRange)
            inRange = true;
        else
            inRange = false;

        return inRange;
    }

    public bool InRange()
    {
        return inRange;
    }

    private void EnemyHighlighted()
    {
        if (inRange && !hit)
            this.gameObject.GetComponent<Renderer>().material = states[1];
        else if (!hit)
            SetDefaultMaterial();
    }

    private void EnemyHit()
    {
        if (hit)
            this.gameObject.GetComponent<Renderer>().material = states[2];
        Invoke("SetDefaultMaterial", flashDamage);
    }

    private void SetDefaultMaterial()
    {
        this.gameObject.GetComponent<Renderer>().material = states[0];
        hit = false;
    }

    public void EnemyDamaged(int damage)
    {
        hit = true;
        EnemyHit();
        pv.TakeDamage(damage);
        if (pv.currHealth <= 0 && !isDead)
        {
            anim.Play("Enemy_death");
            //this.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
            isDead = true;
        }
        else if (pv.currHealth >= 1)
            sndmngr.Play("enemyimpact");
    }

    public void EnemyDeactivate()
    {
        sndmngr.Play("enemyshatter");
        this.gameObject.GetComponentInParent<JButler_Agent>().gameObject.SetActive(false);
    }

    public void ResetDefaults()
    {
        transform.localScale = new Vector3(1, 1, 1);
        pv.currHealth = pv.maxHealth;
        SetDefaultMaterial();
        isDead = false;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool HitTaken()
    {
        return hit;
    }

    //////////////////////////////////////////////////
    // Enemy > Player Methods
    //////////////////////////////////////////////////

    private bool PlayerIsHit()
    {
        if (!phit)
            phit = true;
        else
        {
            phit = false;
        }
        return phit;
    }

    private Material DefaultMat()
    {
        return mats.DefaultMat();
    }

    //////////////////////////////////////////////////
    // Enemy > Player Methods (Melee)
    //////////////////////////////////////////////////

    public void TriggerCheckMelee(Collider other)
    {
        if (other.gameObject == player.gameObject && melee)
        {
            ConstantDamage();
        }
    }

    public void EnemyMelee()
    {
        if (PlayerIsHit())
        {
            player.TakeDamage(meleeDamage);
            mats.HitMat();
            Invoke("DefaultMat", ttk);
        }
    }

    // Creates constant and consistent damage.
    public void ConstantDamage()
    {
        timer += Time.deltaTime;
        if (PlayerIsHit() && timer >= ttk)
        {
            player.TakeDamage(meleeDamage);
            mats.HitMat();
            timer = 0.0f;
            Invoke("DefaultMat", ttk);
        }
    }

    //////////////////////////////////////////////////
    // Enemy > Player Methods (Shooting)
    //////////////////////////////////////////////////

    public void ShowHit()
    {
        mats.HitMat();
        Invoke("DefaultMat", flashDamage);
    }

    public void WeFire()
    {
        if (shooter)
            shooting.ShootProjectile();
    }

    //////////////////////////////////////////////////
    // Enemy > Player Methods (Exploding)
    //////////////////////////////////////////////////

    public void TriggerCheckExplosion(Collider other)
    {
        if ((other.gameObject.GetComponent<Player_Control>() || other.gameObject.GetComponentInChildren<Enemy>()) && exploder)
            Explode(other);
        if (other.gameObject.GetComponent<FloorReference>() && exploder)
            DamageLevel();
    }

    private void Explode(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Control>())
        {
            player.TakeDamage(explosionDamage);
            mats.HitMat();
            Invoke("DefaultMat", ttk);
        }

        if (other.gameObject.GetComponentInChildren<Enemy>())
            other.GetComponentInChildren<Enemy>().EnemyDamaged(explosionDamage);

        this.EnemyDamaged(explosionDamage);
    }

    private void DamageLevel()
    {
        ov.LevelDamage(explosionDamage);
        //Debug.Log("Level was Damaged!");
        this.EnemyDamaged(explosionDamage);
    }
}
