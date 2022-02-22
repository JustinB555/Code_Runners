/*
   Editor: Jonathan Angulo
   Created: 11/16/2020

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    Player_Values PV;
    GameObject fires;
    ShieldPickUp barrier;

    bool isfire;
    bool infire;
    public bool firA;

    public float onfire = 0.2f;
    public float firE = 3.0f;
    public float dam = 0.2f;

    public void Start()
    {
        

        PV = GameObject.Find("Player_EGO").GetComponent<Player_Values>();
        isfire = false;
        firA = false;

        fires = GameObject.Find("Player Node/Player_EGO/Player/Fire");
        fires.SetActive(false);

        barrier = GameObject.Find("PickUpShield").GetComponent<ShieldPickUp>();
        
    }

    private void Update()
    {

        if (firA)
        {
            
            firE -= Time.deltaTime;
            
            if (firE >= 0.1f)
            {
                
                onfire -= Time.deltaTime;
                if(onfire <= 0.0f && isfire)
                {
                    
                    PV.TakeDamage(1);
                    onfire = 0.2f;
                }

                
            }
            else if(firE <= 0.0f)
            {
                fires.SetActive(false);
                isfire = false;
                firE = 3.0f;
                firA = false;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Values>())
        {
            //Debug.Log("Fire");
            isfire = true;
            infire = true;
            fires.SetActive(true);
            

            onfire -= Time.deltaTime;
            if(onfire <= 0.0f && isfire)
            {
                PV.TakeDamage(1);
                onfire = 0.2f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Values>())
        {
            infire = false;

            FireEffect();
        }
    }


    public void FireEffect()
    {
        firA = true;
        isfire = true;
        fires.SetActive(true);

    }

    public void Flames()
    {
        fires.SetActive(false);
    }
}
