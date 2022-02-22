/*
 * Editor: Jonathan Angulo
 * Date of Creation: 11/9/2020
   Removed 11/12/2020
   This was removed with the Objective Feature for Milestone 3.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject instructions = null;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Switch")
        {
            instructions.SetActive(true);
            Animator anim = other.GetComponentInChildren<Animator>();
            if(Input.GetKeyDown(KeyCode.R))
                anim.SetTrigger("TurnOn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Switch")
        {
            instructions.SetActive(false);
        }

    }
}
*/
