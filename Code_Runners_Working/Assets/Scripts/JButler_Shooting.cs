//////////////////////////////////////////////////
// Credits
// Referenced Script: Anthony Lowder
//                    GravityWell.cs:199 "SCRAPS Project" Project and Portfolio III: Game Design C202005 00
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// Deals with how the projectile is shot throw the air.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_Shooting : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("Object Reference")]
    [Tooltip("The ammoPool that you will be pulling from.\nMUST SETUP BEFOREHAND!!!")]
    [SerializeField]
    private JButler_ObjectPool ammoPool = null;

    [Header("Projectile Speed")]
    [Tooltip("Deals with how fast you want the projectile to leave the spawn point.")]
    [SerializeField]
    private float pushForce = 1.0f;

    private Rigidbody rb = null;
    SoundManager sndmngr = null;

    public void Start()
    {
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }

    public void ShootProjectile()
    {
        try
        {
            GameObject ammo = ammoPool.GetObjectFromPool();
            rb = ammo.GetComponent<Rigidbody>();
            ammo.GetComponent<Projectile>().ourShooter = this;
            ammo.transform.position = ammoPool.transform.position;
            ammo.transform.rotation = ammoPool.transform.rotation;
            #region // Debugging
        //Debug.Log("Ammo position = " + ammo.transform.position);
        //Debug.Log("Ammo rotation = " + ammo.transform.rotation);
        #endregion
            ammo.SetActive(true);
            sndmngr.Play("enemypop");

            rb.velocity = Vector3.zero;
            Vector3 direction = transform.InverseTransformDirection(ammo.transform.forward);
            //Debug.Log("Direction = " + direction);
            Vector3 force = direction * (pushForce * rb.mass);
            //Debug.Log("Force = " + force);
            rb.AddForce(force, ForceMode.Impulse);

            rb = null;
        }
        catch (System.NullReferenceException e)
        {
            throw new System.Exception(name + " has ran out of ammo!\n" + e.Message);
        }
    }
}
