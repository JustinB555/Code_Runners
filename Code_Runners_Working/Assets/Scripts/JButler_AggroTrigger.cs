//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Controls when to call the StateMachine to start Pursuing the player. It also will allow the 
// Exploder to continuously pursue the player.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_AggroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        JButler_Agent otherAgent = other.GetComponent<JButler_Agent>();
        if (otherAgent != null && !otherAgent.FindPursuers() && !other.GetComponentInChildren<Enemy>().IsDead() && !otherAgent.IsExploding())
            otherAgent.PursuePlayer();
    }

    private void OnTriggerExit(Collider other)
    {
        JButler_Agent otherAgent = other.GetComponent<JButler_Agent>();
        if (otherAgent != null && !otherAgent.FindPursuers() && !other.GetComponentInChildren<Enemy>().IsDead() && !otherAgent.Exploder())
            otherAgent.StopPursuing();
    }
}
