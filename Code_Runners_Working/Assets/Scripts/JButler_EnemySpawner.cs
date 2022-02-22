//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 16, 2020
// Description:
// Controls which agents spawn when hitting a trigger volume.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_EnemySpawner : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("Agents to Spawn")]
    [Tooltip("Put what agents you want to spawn here.")]
    [SerializeField] private JButler_Agent[] agents;

    [Header("Checks")]
    [Tooltip("Checks to see if this agent has already spawned, preventing from triggering again.")]
    [SerializeField] private bool spawned = false;
    [Tooltip("On the other hand, if you want to keep spawning them you can.")]
    [SerializeField] private bool continues = false;
    //[Tooltip("Used with DEBUG to toggle when to see the colliders.")]
    //[SerializeField] private bool spawnCollider = false;

    private SpawnCollider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        GrabColliders();
        CheckIndex();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    SpawnCollider();
    }

    //////////////////////////////////////////////////
    // Methods
    //////////////////////////////////////////////////

    private void CheckIndex()
    {
        try
        {
            agents[0].GetComponent<JButler_Agent>();
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.Exception(name + " has no agents to spawn!\tDid you make sure to add agents to the spawner?\n" + e.Message);
        }

        try
        {
            colliders[0].GetComponent<Renderer>();
        }
        catch (System.IndexOutOfRangeException e)
        {
            throw new System.Exception(name + " could not find active colliders!\tDid you turn off the colliders?\nMake sure to turn on desired colliders and turn of the renderers instead.\n" + e.Message);
        }
    }

    private void GrabColliders()
    {
        colliders = FindObjectsOfType<SpawnCollider>();
    }

    // Wyatt, grab this method for DEBUG.
    public void SpawnCollider(bool spawnCollider)
    {
        GrabColliders();
        if (spawnCollider)
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].gameObject.GetComponent<Renderer>().enabled = true;
        else
            for (int i = 0; i < colliders.Length; i++)
                colliders[i].gameObject.GetComponent<Renderer>().enabled = false;
    }

    private void ContinuesSpawn()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].GetComponentInChildren<Enemy>().ResetDefaults();
            agents[i].gameObject.SetActive(true);
        }
    }

    private void SpawnEnemy()
    {
        if (!spawned)
        {
            for (int i = 0; i < agents.Length; i++)
                if (!agents[i].GetComponentInChildren<Enemy>().IsDead())
                    agents[i].gameObject.SetActive(true);
            spawned = true;
        }
    }

    //////////////////////////////////////////////////
    // Collsions Events
    //////////////////////////////////////////////////

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Control>() && !continues)
            SpawnEnemy();
        else if (other.GetComponent<Player_Control>() && continues)
            ContinuesSpawn();
    }
}
