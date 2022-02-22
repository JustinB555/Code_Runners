//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// A basic scipt for changing how the player looks when they get hit.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_PlayerMat : MonoBehaviour
{
    [Header("Player's Material")]
    [Tooltip("Setup the materials you want to use for the player\n0) Default \n1) Hit")]
    [SerializeField] private Material[] states = null;

    public Material DefaultMat()
    {
        return this.gameObject.GetComponent<Renderer>().material = states[0];
    }

    public Material HitMat()
    {
        return this.gameObject.GetComponent<Renderer>().material = states[1];
    }
}
