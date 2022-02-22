using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLTrigger : MonoBehaviour
{
    Scene_Manager scnmngr = null;
    Overlord overlord = null;

    private void Start()
    {
        scnmngr = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overlord.Winning();
            StartCoroutine(WinningBuffer());
        }
    }

    IEnumerator WinningBuffer()
    {
        yield return new WaitForSeconds(2);
        scnmngr.NextLevel();
    }
}
