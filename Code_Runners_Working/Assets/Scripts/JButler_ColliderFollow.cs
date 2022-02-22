//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 13, 2020
// Description:
// A simple script so that the colliders needed for triggers can follow the player.
// This prevents multiple colliders triggering eachother.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_ColliderFollow : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    private Player_Control player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPlayer();
    }

    //////////////////////////////////////////////////
    // Methods
    //////////////////////////////////////////////////

    private void MoveWithPlayer()
    {
        transform.position = player.transform.position;
    }

}
