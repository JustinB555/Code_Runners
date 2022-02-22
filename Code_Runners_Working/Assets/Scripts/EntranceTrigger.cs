using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject entrance = null;

    SoundManager sndmngr = null;

    bool hasSoundplayed = false;

    [SerializeField]
    GameObject[] enemies = null;
    void Start()
    {
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        entrance.GetComponent<Transform>().localPosition = new Vector3(0, -4, 0);
        hasSoundplayed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSoundplayed)
        {
            sndmngr.Play("DoorOpen");
            entrance.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(false);
            }
            hasSoundplayed = true;
        }
    }
}
