//////////////////////////////////////////////////
// Credits
// Base Credits: Justin Butler
//               AgentManager.cs "Sneaky Sneaky" Game Development C202009 00
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Responsible with keeping track of AI agents and NavPoints.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JButler_AgentManager : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("Agents")]
    [Tooltip("Keeps track of all agents in the scene.")]
    [SerializeField]
    JButler_Agent[] agents = null;

    [Header("Nav Points")]
    [Tooltip("Keeps track of all Nav Points in the scene.")]
    [SerializeField]
    NavPoint[] navPoints = null;

    // Start is called before the first frame update
    void Start()
    {
        agents = FindObjectsOfType<JButler_Agent>();
        navPoints = FindObjectsOfType<NavPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        //ShowNavPoints();
    }

    //////////////////////////////////////////////////
    // Methods
    //////////////////////////////////////////////////

    private void ShowNavPoints()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    ShowNavPointsToggle();
    }

    public void ShowNavPointsToggle(bool isVisable)
    {
        if (isVisable)
            for (int i = 0; i < navPoints.Length; i++)
                navPoints[i].gameObject.GetComponent<Renderer>().enabled = true;
        else
            for (int i = 0; i < navPoints.Length; i++)
                navPoints[i].gameObject.GetComponent<Renderer>().enabled = false;
    }
}
