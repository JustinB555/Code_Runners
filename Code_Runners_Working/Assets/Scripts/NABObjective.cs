using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NABObjective : MonoBehaviour
{
    Scene_Manager scnmngr = null;
    LevelHealthObjectiveScript lhos = null;
    SoundManager sndmngr = null;
    void Start()
    {
        scnmngr = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        lhos = GameObject.Find("Useless Collectibles").GetComponent<LevelHealthObjectiveScript>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player_Control>() && scnmngr.sceneIndex <= 2)
        {
            sndmngr.Play("Collect");
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.GetComponent<Player_Control>() && scnmngr.sceneIndex == 3)
        {
            sndmngr.Play("Collect");
            lhos.objectivesCollected = lhos.objectivesCollected += 1;
            gameObject.SetActive(false);
        }

    }
}
