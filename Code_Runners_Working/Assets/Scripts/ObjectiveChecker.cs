/* 
   Editor: Jacob Arnold
   Date of Creation: 11/3/2020
   Removed 11/12/2020 by Jonathan Angulo
   This was removed since the Objective feature was removed for Milestone 3

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveChecker : MonoBehaviour
{
    Overlord Overlord;
    SoundManager sndmngr = null;
    void Start()
    {
        Overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        
    }

    public void ObjComplete()
    {
        Overlord.CompleteObjective();
    }

    public void TaskProcess()
    {
        sndmngr.Play("TaskProcess");
    }
    public void TaskPass()
    {
        sndmngr.Play("TaskPass");
    }
}
*/