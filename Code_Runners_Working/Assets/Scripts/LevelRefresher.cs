using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRefresher : MonoBehaviour
{
    [SerializeField]
    Overlord overlord = null;

    bool isActive = false;

    private void Start()
    {
        isActive = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.timeScale > 0)
        {
            isActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            overlord.levelCurrHealth = overlord.levelCurrHealth += 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Time.timeScale > 0)
        {
            isActive = false;
        }
    }
}
