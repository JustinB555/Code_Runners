/*
 * Editor: Wyatt Curtiss
 * Date of Creation: October 31, 2020
 * Note: Getting rid of shield for milestone 2. 
 *       Commented everything out to make sure the script would never be used. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //[SerializeField]
    //GameObject ShieldObject = null;
    //[SerializeField]
    //float ShieldActiveTime = 0f;
    //[SerializeField]
    //float ShieldCooldownTime = 0f;
    //[SerializeField]
    //KeyCode shieldButton = KeyCode.Q;

    //public bool ShieldActive = false;
    //private bool CooldownActive = false;
    //void Start()
    //{
    //    ShieldActive = false;
    //    ShieldObject.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(shieldButton))
    //    {
    //        //Debug.Log("Player pressed " + shieldButton);
    //        if (!CooldownActive)
    //        {
    //            ShieldActive = true;
    //            ShieldToggle();
    //            Debug.Log("Shield will be active for " + ShieldActiveTime + "seconds.");
    //            Debug.Log("Shield will be recharged in " + ShieldCooldownTime + "seconds.");
    //            StartCoroutine(CooldownTimer());
//    //        }
//    //    }
//    //}

//    public void ShieldToggle()
//    {
//        ShieldObject.SetActive(true);
//        //Debug.Log("Shield was toggled");
//        StartCoroutine(ActiveTimer());
//    }

//    IEnumerator ActiveTimer()
//    {
//        yield return new WaitForSeconds(ShieldActiveTime);
//        ShieldObject.SetActive(false);
//        ShieldActive = false;
//        //Debug.Log("shield active is false");
//    }
//    IEnumerator CooldownTimer()
//    {
//        CooldownActive = !CooldownActive;
//        //Debug.Log("cooldown timer started");
//        yield return new WaitForSeconds(ShieldCooldownTime);
//        CooldownActive = !CooldownActive;
//        Debug.Log("cooldown timer ended");
//    }
}
