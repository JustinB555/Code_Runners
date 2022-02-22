/*
   Removed 11/12/2020 by Jonathan Angulo
   This was removed with the Objective feature for MileStone 3.



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Interact : MonoBehaviour
{
    public GameObject instructions = null;
    [SerializeField] Collider ourCollider = null;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "PipeCorner")
        {
            instructions.SetActive(true);
            Animator anim = other.GetComponentInChildren<Animator>();
            if(Input.GetKeyDown(KeyCode.R))
                anim.SetTrigger("Rotate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PipeCorner")
        {
            instructions.SetActive(false);
        }
    }
}
*/