//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// A simple reference to keep track of wall damage.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Overlord ov = null;

    private void Start()
    {
        ov = FindObjectOfType<Overlord>();
    }

    public void WallDamage(int damage)
    {
        ov.LevelDamage(damage);
    }
}
