using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRevealer : MonoBehaviour
{
    [SerializeReference]
    GameObject levelHider = null;
    bool isHiding = true;
    // Start is called before the first frame update
    void Start()
    {
        levelHider.SetActive(true);
        isHiding = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isHiding)
        {
            levelHider.SetActive(false);
            isHiding = false;
        }
    }
}
