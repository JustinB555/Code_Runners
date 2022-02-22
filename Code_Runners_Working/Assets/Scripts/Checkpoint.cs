using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public bool playerIsInside = false;
    public float saveIcontime = 2f;
    DebugScript debug = null;
    OptionValueStore opVlSt = null;
    [SerializeField]
    GameObject saving = null;
    Overlord overlord = null;

    SoundManager sndmngr = null;

    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        debug = GameObject.Find("DEBUG").GetComponent<DebugScript>();
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        saving.SetActive(false);
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Player_Control>() && overlord.playerAlive)
        {
            playerIsInside = true;
            debug.UpdateCheckpoint();
            opVlSt.CheckpointData();
            opVlSt.Save();
            saving.SetActive(true);
            StartCoroutine(SaveBuffer());
            opVlSt.Load();
            sndmngr.Play("CP");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }

    IEnumerator SaveBuffer()
    {
        yield return new WaitForSeconds(saveIcontime);
        saving.SetActive(false);
    }
}
