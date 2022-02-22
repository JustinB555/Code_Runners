/*
 * Editor: Jonathan Angulo
 * Created: 11/14/2020
 * 
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    Player_Values player_Values;

    Collider health_collider;

    SoundManager sndmngr = null;

    public int healthBonus = 10;
    

    void Awake()
    {
        health_collider = GetComponent<Collider>();
        
        player_Values = GameObject.Find("Player_EGO").GetComponent<Player_Values>();

        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player_Values.currHealth < player_Values.maxHealth)
        {
            
            player_Values.TakeDamage(healthBonus * -1);

            if (other.CompareTag("Player"))
            {

                Destroy(gameObject);
            }

        }
        
        
        if (other.CompareTag("Player"))
        {
            sndmngr.Play("Health");
            Destroy(gameObject);
        }
    }
 





}
