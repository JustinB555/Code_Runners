//////////////////////////////////////////////////
// Credits
// Base Credits: Justin Butler
//               JMB_ObjectPool.cs "SCRAPS Project" Project and Portfolio III: Game Design C202005 00
// Creator: Justin Butler
// Created: November 3, 2020
// Description:
// A generic Object Pool that can be used for anything.
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JButler_ObjectPool : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Fields
    //////////////////////////////////////////////////

    [Header("Object Pool")]
    [Tooltip("These are the objects that you can use from the pool. If your code is right, they shouldn't run out.")]
    public List<GameObject> pooledObjects;
    [Tooltip("This is the object that you want to pool, HAS TO BE A GAME OBJECT!!!")]
    public GameObject objectToPool;
    [Tooltip("State how many of these objects you want.")]
    public int amountToPool;

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
