/*
 * Editor: Wyatt Curtiss
 * Date of Creation: October 31, 2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject PauseMenu = null;

    private bool isPaused = false;

    private string currScene = null;

    SoundManager sndmngr = null;
    OptionValueStore opVlSt = null;

    void Start()
    {
        Resume();
        PauseMenu.SetActive(false);
        isPaused = false;
        sndmngr = GameObject.Find("AudioManager").GetComponent<SoundManager>();
        opVlSt = GameObject.Find("OptionValueStore").GetComponent<OptionValueStore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            currScene = SceneManager.GetActiveScene().name;
            
            if (!isPaused && currScene != "MainMenu")
            {
                PauseGame();
                sndmngr.Play("Flick");
            }
            else if(isPaused)
            {
                Resume();
                sndmngr.Play("Flick");
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuToggle()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        opVlSt.Save();
    }
}
