/* 
   Editor: Jacob Arnold, Justin Butler
   Date of Creation: 11/2/2020
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Values : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;
    public bool isImmortal = false;

    public Main_UI MUI;
    public bool usingUI = false;
    Overlord Overlord;
    [SerializeField]
    SoundManager sndmngr = null;
    private void Awake()
    {
        currHealth = maxHealth;
        if (usingUI)
            MUI.SetMaxHealth(maxHealth);
        
    }
    void Start()
    {

        Overlord = GameObject.Find("Overlord").GetComponent<Overlord>();

        isImmortal = false;
    }

    void Update()
    {
        // Keep HealthPickUp from going over MaxHP
        if(currHealth > 100)
        {
            currHealth = 100;
        }
        
        
        
        #region
        //Testing Only
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    TakeDamage(15);
        //}
        #endregion
    }

    public void TakeDamage(int damage)
    {
        if (!isImmortal)
        {
            currHealth -= damage;

            sndmngr.Play("Ouch");

            if (usingUI)
                MUI.SetHealth(currHealth);

            if (currHealth <= 0 && this.gameObject.GetComponent<Player_Control>())
            {
                Overlord.playerAlive = false;
                MUI.hImage.color = Color.grey;
            }
        }
        else
            return;
    }
}
