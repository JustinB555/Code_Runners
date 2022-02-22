//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// Used on the projectiles to determine a) What did they hit AND b) to turn back off.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    private Enemy e = null;
    private Wall w = null;
    private Player_Control player = null;
    private Shield s = null;

    [Header("Object References")]
    [Tooltip("Used by enemies to determine who shot.")]
    public JButler_Shooting ourShooter = null;

    [Header("Combat")]
    [Tooltip("The amount of damage the projectile does once it hits something.\nUsed by: Player, Roamer, and Turret")]
    [SerializeField] private int projectileDamage = 10;
    [Tooltip("Determines if this is an enemy projectile or not.")]
    [SerializeField] private bool enemyP = false;

    private SoundManager sndmngr = null;


    //////////////////////////////////////////////////
    // Class Assignment
    //////////////////////////////////////////////////

    private void Start()
    {
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();   
    }

    //////////////////////////////////////////////////
    // Collision Events
    //////////////////////////////////////////////////

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Enemy>() && !enemyP)
        {
            //Debug.Log("Enemey Hit!");
            e = other.gameObject.GetComponentInChildren<Enemy>();
            e.EnemyDamaged(projectileDamage);
            e = null;
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.GetComponent<Player_Control>() && enemyP)
        {
            //Debug.Log("Player Hit!");
            e = ourShooter.GetComponentInChildren<Enemy>();
            player = other.gameObject.GetComponent<Player_Control>();
            player.pv.TakeDamage(projectileDamage);
            e.ShowHit();
            player = null;
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.GetComponent<Wall>() && !enemyP)
        {
            //Debug.Log("Wall Hit!");
            sndmngr.Play("wallhit");
            w = other.gameObject.GetComponent<Wall>();
            w.WallDamage(projectileDamage);
            w = null;
            this.gameObject.SetActive(false);
        }
        else if (other.gameObject.GetComponent<Wall>() && enemyP)
        {
            //Debug.Log("Wall Hit!");
            sndmngr.Play("wallhit");
            this.gameObject.SetActive(false);
        }

        if (other.gameObject.GetComponent<Shield>() && enemyP)
        {
            //Debug.Log("Shield Hit!");
            sndmngr.Play("wallhit");
            s = other.gameObject.GetComponent<Shield>();
            s = null;
            this.gameObject.SetActive(false);
        }

    }
}
