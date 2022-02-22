//////////////////////////////////////////////////
// Credits
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// Responsible with how the player deals melee damage to an enemy.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_Melee : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    private bool nearEnemy = false;
    private Dictionary<GameObject, Enemy> thoseInRange = new Dictionary<GameObject, Enemy>();

    [Header("Combat")]
    [Tooltip("How much damage to do want to deal?\nUsed by: Player")]
    [SerializeField] private int meleeDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////////////////////////////////////////////////
    // Triggers
    //////////////////////////////////////////////////

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Enemy>() && !CheckInRange(other))
        {
            AddInRange(other);
            other.gameObject.GetComponentInChildren<Enemy>().InMeleeRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<Enemy>() && CheckInRange(other))
        {
            RemoveInRange(other);
            other.gameObject.GetComponentInChildren<Enemy>().InMeleeRange(false);
        }
    }

    //////////////////////////////////////////////////
    // Methods
    //////////////////////////////////////////////////

    public bool NearEnemy()
    {
        foreach (KeyValuePair<GameObject,Enemy> pair in thoseInRange)
        {
            if (pair.Value.InRange())
                return nearEnemy = true;
            else
                continue;
        }
        return nearEnemy = false;
    }

    public void MeleeDamage()
    {
        foreach (KeyValuePair<GameObject,Enemy> pair in thoseInRange)
            pair.Value.EnemyDamaged(meleeDamage);
    }

    private void AddInRange(Collider other)
    {
        thoseInRange.Add(other.gameObject, other.gameObject.GetComponentInChildren<Enemy>());
    }

    private void RemoveInRange(Collider other)
    {
        thoseInRange.Remove(other.gameObject);
    }

    private bool CheckInRange(Collider other)
    {
        foreach (var name in thoseInRange)
        {
            if (name.Key.name == other.gameObject.name)
                return true;
            else
                continue;
        }
        return false;
    }
}
