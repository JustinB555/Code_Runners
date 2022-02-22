﻿//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Controls when to call the StateMachine to start Shooting at the player.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_AggroShooter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        JButler_Agent otherAgent = other.GetComponent<JButler_Agent>();
        if (otherAgent != null && !otherAgent.FindRoamer() && !other.GetComponentInChildren<Enemy>().IsDead())
        {
            otherAgent.FireFromADistance();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        JButler_Agent otherAgent = other.GetComponent<JButler_Agent>();
        if (otherAgent != null && !otherAgent.FindRoamer() && !other.GetComponentInChildren<Enemy>().IsDead())
        {
            otherAgent.StopPursuing();
        }
    }
}
