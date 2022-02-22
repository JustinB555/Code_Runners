using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHealthObjectiveScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectiveNodes = null;
    [SerializeField]
    GameObject door = null;
    public int objectivesCollected = 0;
    SoundManager sndmngr = null;
    bool hasSoundPlayed = false;
    void Start()
    {
        objectivesCollected = 0;
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        hasSoundPlayed = false;
    }

    private void FixedUpdate()
    {
        if (objectivesCollected >= objectiveNodes.Length && !hasSoundPlayed)
        {
            PlayDoorSound();
            hasSoundPlayed = true;
        }
    }

    private void PlayDoorSound()
    {
        sndmngr.Play("DoorOpen");
        door.GetComponent<Transform>().position = new Vector3(0, -20, 0);
    }
}
