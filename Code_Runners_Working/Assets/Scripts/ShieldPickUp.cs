/*
 * Editor: Jonathan Angulo
 * Created: 11/14/2020
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    [SerializeField]
    GameObject PickUp;
    GameObject shield;

    Collider shield_collider;
    SoundManager sndmngr = null;

    public float waitTime = 5.0f;
    public bool isActive = false;
    bool hasSoundPlayed = false;
    
    void Start()
    {
        shield_collider = GetComponent<Collider>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        
        shield = GameObject.Find("Player Node/Player_EGO/Player/Shield");
        shield.SetActive(false);
        waitTime = 5f;
        hasSoundPlayed = false;
    }

    private void Update()
    {
        //waitTime -= Time.deltaTime;
        if (isActive == true)
        {
            waitTime -= Time.deltaTime;
            if(waitTime <= 0f)
            {
                shield.SetActive(false);

                isActive = false;

                waitTime = 5f;
            }           
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            

            ShieldActivate();

            shield_collider.enabled = !shield_collider.enabled;
            PickUp.SetActive(false);
            
            //Destroy(gameObject);
        }
    }

    public void ShieldActivate()
    {
        shield.SetActive(true);

        isActive = true;
        if (!hasSoundPlayed)
        {
            sndmngr.Play("Shield");
            Invoke("HasSoundPlayedToggle", .2f);
        }
        

        //shield.SetActive(false);


        //Destroy(shield, 5);
    }

    private void HasSoundPlayedToggle()
    {
        hasSoundPlayed = false;
    }

   
}
